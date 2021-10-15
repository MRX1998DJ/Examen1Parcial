using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PM2E17409.Modals
{
    class Mapeo
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }

        [MaxLength(100)]
        public string DescripcionLarga { get; set; }

        [MaxLength(30)]
        public string DescripcionCorta { get; set; }

    }
}
