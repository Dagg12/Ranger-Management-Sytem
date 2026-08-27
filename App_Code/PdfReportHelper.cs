using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web;

namespace TheRanger
{
    public static class PdfReportHelper
    {
        public class Row
        {
            public string Id, Type, Guest, Experience, Location, Date, Guests, Total, Status, BookedOn;
        }

        public static byte[] BuildBookingsReport(string reportTitle, string ownerName, IList<Row> rows)
        {
            var pages = new List<string>();
            var page = new StringBuilder();
            int y = 675;

            Action startPage = () =>
            {
                page.Append("q 0.063 0.157 0.110 rg 0 735 595 107 re f Q\n");
                page.Append("q 0.86 0.75 0.48 rg 0 735 595 5 re f Q\n");
                page.Append("BT /F2 24 Tf 50 795 Td (THE RANGER) Tj ET\n");
                page.Append("BT /F1 9 Tf 50 778 Td (SAFARI MANAGEMENT SYSTEM) Tj ET\n");
                page.Append("BT /F2 15 Tf 50 708 Td (" + Escape(reportTitle) + ") Tj ET\n");
                page.Append("BT /F1 8 Tf 50 691 Td (Owner: " + Escape(ownerName) + ") Tj ET\n");
                page.Append("BT /F1 8 Tf 350 691 Td (Generated: " + Escape(DateTime.Now.ToString("dd MMM yyyy HH:mm")) + ") Tj ET\n");
                y = 650;
                page.Append("q 0.86 0.75 0.48 rg 42 " + y + " 511 22 re f Q\n");
                string[] headers = { "ID", "GUEST", "TYPE / EXPERIENCE", "LOCATION", "DATE", "GUESTS", "TOTAL", "STATUS" };
                int[] xs = { 48, 70, 150, 285, 355, 423, 465, 520 };
                for (int i = 0; i < headers.Length; i++)
                    page.Append("BT /F2 7 Tf " + xs[i] + " " + (y + 7) + " Td (" + Escape(headers[i]) + ") Tj ET\n");
                y -= 28;
            };

            startPage();
            foreach (var r in rows)
            {
                if (y < 75) { pages.Add(page.ToString()); page = new StringBuilder(); startPage(); }
                if ((rows.IndexOf(r) & 1) == 0) page.Append("q 0.96 0.95 0.92 rg 42 " + (y - 5) + " 511 27 re f Q\n");
                string[] vals = {
                    Short(r.Id, 6), Short(r.Guest, 18), Short(r.Type + " / " + r.Experience, 24),
                    Short(r.Location, 14), Short(r.Date, 14), Short(r.Guests, 6), "R " + Short(r.Total, 11), Short(r.Status, 11)
                };
                int[] xs = { 48, 70, 150, 285, 355, 423, 465, 520 };
                for (int i = 0; i < vals.Length; i++)
                    page.Append("BT /F1 7 Tf " + xs[i] + " " + y + " Td (" + Escape(vals[i]) + ") Tj ET\n");
                y -= 30;
            }
            if (rows.Count == 0)
                page.Append("BT /F1 9 Tf 50 " + y + " Td (No bookings match the selected filters.) Tj ET\n");
            page.Append("BT /F1 7 Tf 50 38 Td (The Ranger - Safari Management System | Filtered report) Tj ET\n");
            if (page.Length > 0) pages.Add(page.ToString());
            return BuildPdf(pages);
        }

        private static string Short(string value, int max)
        {
            if (value == null) return "";
            value = value.Trim();
            if (value.Length <= max) return value;
            return value.Substring(0, Math.Max(0, max - 3)) + "...";
        }

        private static string Escape(string value)
        {
            if (value == null) return "";
            value = HttpUtility.HtmlDecode(value).Replace("\\", "\\\\").Replace("(", "\\(").Replace(")", "\\)");
            var sb = new StringBuilder();
            foreach (char c in value)
            {
                if (c >= 32 && c <= 126) sb.Append(c);
                else if (c == '\u2013' || c == '\u2014') sb.Append('-');
                else if (c == '\u2019' || c == '\u2018') sb.Append('\'');
                else if (c == '\u201c' || c == '\u201d') sb.Append('"');
                else sb.Append('?');
            }
            return sb.ToString();
        }

        private static byte[] BuildPdf(List<string> streams)
        {
            var objects = new List<string>();
            objects.Add("<< /Type /Catalog /Pages 2 0 R >>");

            int pageCount = streams.Count == 0 ? 1 : streams.Count;
            int font1Id = 3 + pageCount * 2;
            int font2Id = font1Id + 1;
            var kids = new StringBuilder();
            for (int i = 0; i < pageCount; i++)
                kids.Append(3 + i * 2).Append(" 0 R ");
            objects.Add("<< /Type /Pages /Kids [" + kids + "] /Count " + pageCount + " >>");

            if (streams.Count == 0) streams.Add("BT /F1 11 Tf 50 770 Td (No bookings match the selected filters.) Tj ET\n");

            for (int i = 0; i < streams.Count; i++)
            {
                int pageId = 3 + i * 2;
                int streamId = pageId + 1;
                objects.Add("<< /Type /Page /Parent 2 0 R /MediaBox [0 0 595 842] /Resources << /Font << /F1 " + font1Id + " 0 R /F2 " + font2Id + " 0 R >> >> /Contents " + streamId + " 0 R >>");
                string stream = streams[i];
                objects.Add("<< /Length " + Encoding.ASCII.GetByteCount(stream) + " >>\nstream\n" + stream + "endstream");
            }
            objects.Add("<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica >>");
            objects.Add("<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica-Bold >>");

            var pdf = new StringBuilder();
            pdf.Append("%PDF-1.4\n");
            var offsets = new List<int> { 0 };
            for (int i = 0; i < objects.Count; i++)
            {
                offsets.Add(Encoding.ASCII.GetByteCount(pdf.ToString()));
                pdf.Append(i + 1).Append(" 0 obj\n").Append(objects[i]).Append("\nendobj\n");
            }
            int xref = Encoding.ASCII.GetByteCount(pdf.ToString());
            pdf.Append("xref\n0 ").Append(objects.Count + 1).Append("\n0000000000 65535 f \n");
            for (int i = 1; i < offsets.Count; i++) pdf.Append(offsets[i].ToString("D10")).Append(" 00000 n \n");
            pdf.Append("trailer\n<< /Size ").Append(objects.Count + 1).Append(" /Root 1 0 R >>\nstartxref\n").Append(xref).Append("\n%%EOF");
            return Encoding.ASCII.GetBytes(pdf.ToString());
        }

    }
}
