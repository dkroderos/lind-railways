using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINDRailways.Model
{
    public class TransportationEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageSource { get; set; }
        public string LargeImageSource { get; set; }

        public TransportationEntity(string name, string description, string imageSource, string largeImageSource)
        {
            this.Name = name;
            this.Description = description;
            this.ImageSource = imageSource;
            this.LargeImageSource = largeImageSource;
        }
    }
}
