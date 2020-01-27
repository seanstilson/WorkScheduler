using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WorkScheduler.Models;

namespace WorkScheduler.Services
{
    public class DesignerService : IDesignerService
    {
        public DesignerService()
        {
        }

        public ObservableCollection<Designer> GetDesigners()
        {
            var assembly = typeof(DesignerService).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("WorkScheduler.Designers.json");

            using (var reader = new System.IO.StreamReader(stream))
            {

                var json = reader.ReadToEnd();
                var data = JsonConvert.DeserializeObject<ObservableCollection<Designer>>(json);
                return data;
            }
        }
    }
}
