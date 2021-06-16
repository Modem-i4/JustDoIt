using JustDoIt.Application.Enums;
using JustDoIt.Application.Filters;
using JustDoIt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace JustDoIt.Application.Features.Tasks.Queries.GetColumnTasks
{
    public class GetColumnTasksParameter
    {
        public int DeskId { get; set; }
        public TaskListModes TaskMode { get; set; }
        public int TAmount { get; set; }
    }
}
