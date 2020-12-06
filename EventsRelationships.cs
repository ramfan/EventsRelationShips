using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Lab1
{
    class EventsRelationships
    {
        private static String getTempory(EntityView e1, EntityView e2)
        {
            if (e1.Equals(e2))
            {
                return "NULL";
            }

            if (e1.rightBorder < e2.leftBorder || e1.leftBorder > e1.rightBorder)
            {
                return "rts";
            }
            else if (e1.rightBorder == e2.leftBorder || e1.leftBorder == e2.rightBorder)
            {
                return "rtsn";
            }
            else if (e1.leftBorder == e2.leftBorder)
            {
                return "rtel";
            }
            else if (e1.rightBorder == e2.rightBorder)
            {
                return "rter";
            }
            else if (
                (e1.leftBorder > e2.leftBorder && e1.rightBorder < e2.rightBorder) ||
                (e1.leftBorder < e2.leftBorder && e1.rightBorder > e2.rightBorder)
                )
            {
                return "rte";
            }

            return "rtes";
        }

        private readonly List<EntityView> eventList;
        private readonly Dictionary<EntityView, Dictionary<EntityView, String>> relationships = new Dictionary<EntityView, Dictionary<EntityView, String>>();
        
        public EventsRelationships(List<EntityView> eventList)
        {
            this.eventList = eventList;
            InitTable();
        }

        private void InitTable()
        {
            foreach(EntityView entity in eventList)
            {
                relationships.Add(entity, new Dictionary<EntityView, String>());
            }

            foreach(KeyValuePair<EntityView, Dictionary<EntityView, String>> relationship in relationships)
            {
                foreach (EntityView entity in eventList)
                {
                    relationship.Value.Add(entity, "NULL");
                }
            }

        }



        public void UpdateTable()
        {
            foreach (KeyValuePair<EntityView, Dictionary<EntityView, String>> relationship in relationships)
            {

                foreach (KeyValuePair<EntityView, String> eventKeyValue in relationship.Value.ToList())
                {
                    String tempory = getTempory(relationship.Key, eventKeyValue.Key);
                    relationship.Value[eventKeyValue.Key] = tempory;
                }
            }
        }   

        public Dictionary<EntityView, Dictionary<EntityView, String>> GetTable()
        {
            return this.relationships;
        }

        public void DisplayData(Grid grid)
        {
            grid.Children.Clear();

            for (var i = 0; i < eventList.Count; i++)
            {
                TextBlock eventRow = new TextBlock
                {
                    Text = eventList[i].name
                };
                Grid.SetRow(eventRow, i + 1);
                Grid.SetColumn(eventRow, 0);
                grid.Children.Add(eventRow);

                for (var j = 0; j < eventList.Count; j++)
                {


                    TextBlock eventCol = new TextBlock();

                    if (i == 0)
                    {
                        eventCol.Text = eventList[j].name;
                        Grid.SetRow(eventCol, 0);
                        Grid.SetColumn(eventCol, j + 1);
                        grid.Children.Add(eventCol);
                    }

                    TextBlock relationship = new TextBlock();
                    relationship.Text = relationships[eventList[i]][eventList[j]];
                    Grid.SetRow(relationship, i + 1);
                    Grid.SetColumn(relationship, j + 1);
                    grid.Children.Add(relationship);                 
                }                    
                
            }
           
        }

    }
}
