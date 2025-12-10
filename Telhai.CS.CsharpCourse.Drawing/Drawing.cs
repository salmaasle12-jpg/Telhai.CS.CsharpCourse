using System;
using System.Collections.Generic;

namespace Telhai.CS.CsharpCourse.Drawing
{
    // ====================== DRAWING ======================
    public class Drawing
    {
        private static int counter = 0;
        public int Id { get; }

        public Drawing()
        {
            counter++;
            Id = counter;
        }

        public virtual double Area()
        {
            return 0;
        }

        public override string ToString()
        {
            return $"Drawing (ID = {Id}, Area = {Area()})";
        }
    }
}