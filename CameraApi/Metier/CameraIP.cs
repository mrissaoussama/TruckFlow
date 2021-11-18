using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CameraApi.Metier
{
    public class CameraIP
    {
        public CameraIP() { }
        public byte[] GetPhoto()
        {
            var rand = new Random();
            var files = Directory.GetFiles("images");
            return (File.ReadAllBytes(files[rand.Next(files.Length)]));
        }
    }
}
