using System;
using System.Collections.Generic;
using System.Text;

namespace JustDoIt.Application.Features.Tasks.Queries.GetColumnTasks
{
    public class GetColumnTasksViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Checked { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
