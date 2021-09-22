using Seshat.Application.TodoLists.Queries.ExportTodos;
using CsvHelper.Configuration;
using System.Globalization;

namespace Seshat.Infrastructure.Files.Maps
{
    public sealed class TodoItemRecordMap : ClassMap<TodoItemRecord>
    {
        public TodoItemRecordMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.Done).Convert(c => c.Value.Done ? "Yes" : "No");
        }
    }
}