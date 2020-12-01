using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.Response;

namespace WebApi.Models.Response
{
    public class ReportResponse: Base
    {
        public List<DateReportItem> DateReportItems { get; set; }
        public List<TicketReportItem> TicketReportItems { get; set; }
    }

    public class DateReportItem {

        public string draw_Name { get; set; }
        public decimal total_tickets { get; set; }
        public decimal total_tickets_cost { get; set; }
        public decimal total_tickets_value { get; set; }
    }

    public class TicketReportItem {

        public string draw_Name { get; set; }
        public decimal total_tickets { get; set; }
        public int userID { get; set; }
        public string UserName { get; set; }
    }
}
