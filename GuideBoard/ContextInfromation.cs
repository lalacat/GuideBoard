using System;

namespace GuideBoard
{
    
    public class ContextInfromation
    {
        private int _id;
        public int ID
        {
            get { return _id; }
            set
            {
                if (value == 0) throw new ArgumentNullException("ID Unset");
                _id = value;
            }
        }
        private int _command;
        public int Command
        {
            get { return _command; }
            set
            {
                if (value == 0) throw new ArgumentNullException("Command Unset");
                _command = value;
            }
        }

        private string _context;

        public string Context
        {
            get { return _context; }
            set
            {
                if (value == null) throw new ArgumentNullException("Context Unset");
                _context = value;
            }
        }
        private string _order;
        public string Order
        {
            get { return _order; }
            set
            {
                if (value == null) throw new ArgumentNullException("Order Unset");
                _order = value;
            }
        }

        private string _direction;
        public string Direction
        {
            get { return _direction; }
            set
            {
                if (value == null) throw new ArgumentNullException("Direction Unset");
                _direction = value;
            }
        }

        private string _degree;
        public string Degree
        {
            get { return _degree; }
            set
            {
                if (value == null) throw new ArgumentNullException("Degree Unset");
                _degree = value;
            }
        }

        public Detail[] Details;

        
        public struct  Detail
        {
            private string _color;
            public string Color
            {
                get { return _color; }
                set
                {
                    if (value == null) throw new ArgumentNullException("Color Unset");
                    _color = value;
                }
            }

            private string _format;
            public string Format
            {
                get { return _format; }
                set
                {
                    if (value == null) throw new ArgumentNullException("Format Unset");
                    _format = value;
                }
            }

            private string _data;
            public string Data
            {
                get { return _data; }
                set
                {
                    if (value == null) throw new ArgumentNullException("Data Unset");
                    _data = value;
                }
            }
           
        }
    }
}