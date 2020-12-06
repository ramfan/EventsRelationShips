using System;

interface IEventEntity
{
    String GetEventInfo(int stage);
}

namespace Lab1
{
   
   public class EventEntity
    {
        public String name { get; }
        public Boolean isSelected { set; get; } = false;
        public int id { get; }
        public int stage { set; get; } = 0;
        public double rightBorder { set; get; } = 0;
        public double leftBorder { set; get; } = 0;

        public EventEntity(int id, String name, Boolean isSelected) : this(id, name)
        {
            this.isSelected = isSelected;
        }

        public EventEntity(int id, String name)
        {
            
            this.name = name;
            this.id = id;
        }

        public override int GetHashCode()
        {
            return this.id;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj == this)
            {
                return true;
            }


            return false;
        }

        public override String ToString()
        {
            switch (stage)
            {
                case 1:
                    return $"{this.name} будет позже";
                case 2:
                    return $"{this.name} выполняется";
                case 3:
                    return $"{this.name} завершилось";
                default:
                    return $"{this.name} не создано";
            }  
        }

    }
}
