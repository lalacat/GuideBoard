using System;

namespace GuideBoard
{
    public class ContextInfromation
    {

        public string Order
        {
            get { return Order; }
            set
            {
                if (value == null) throw new ArgumentNullException("Order Unset");
                Order = value;
            }
        }

        public string Degree
        {
            get { return Degree; }
            set
            {
                if (value == null) throw new ArgumentNullException("Degree Unset");
                Degree = value;
            }
        }
        struct Detail
        {
            public string Color
            {
                get { return Color; }
                set
                {
                    if (value == null) throw new ArgumentNullException("Color Unset");
                    Color=value;
                }
            }

            public string Format
            {
                get { return Format; }
                set
                {
                    if (value == null) throw new ArgumentNullException("Format Unset");
                    Format = value;
                }
            }

            public string Data
            {
                get { return Data; }
                set
                {
                    if (value == null) throw new ArgumentNullException("Data Unset");
                    Data = value;
                }
            }
           
        }
    }
}