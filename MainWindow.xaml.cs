using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Lab1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<EntityView> eventEntityList = new List<EntityView>();
        private readonly static int BLOCK_HEIGHT = 50;
        public List<string> RowHeader { get; } = new List<string>();
        public List<string> ColumnHeader { get; } = new List<string>();
        public List<List<string>> EventValue { get;} = new List<List<string>>();
        private EventsRelationships eventsRelationships;
        public MainWindow()
        {
            InitializeComponent();
            Random rnd = new Random();
            for (int i = 0; i < 5; i++)
            {
                Color randomColor = Color.FromRgb((byte)rnd.Next(i, 256), (byte)rnd.Next(i, 256), (byte)rnd.Next(i, 256));
                EntityView eventEntity = new EntityView(i, $"Событие {i + 1}", randomColor);
                eventListView.Items.Add(eventEntity);
                eventEntityList.Add(eventEntity);
                RowHeader.Add(eventEntity.name);
                ColumnHeader.Add(eventEntity.name);
            }

            eventsRelationships = new EventsRelationships(eventEntityList);

            for (int i = 0; i <= eventEntityList.Count; i++)
            {
                RowDefinition row = new RowDefinition();
                ColumnDefinition column = new ColumnDefinition();
                viewTable.ColumnDefinitions.Add(column);
                viewTable.RowDefinitions.Add(row);

            }
        }

        private void UpdateEventListView(double sliderValue)
        {
            ItemCollection eventListViewItems = eventListView.Items;

            foreach(EntityView itemsControl in eventListViewItems)
            {
                if (sliderValue < itemsControl.leftBorder && sliderValue < itemsControl.rightBorder)
                {
                    itemsControl.stage = 1;
                }

                if (sliderValue > itemsControl.leftBorder && sliderValue < itemsControl.rightBorder)
                {
                    itemsControl.stage = 2;
                }

                if (sliderValue > itemsControl.leftBorder && sliderValue > itemsControl.rightBorder)
                {
                    itemsControl.stage = 3;
                }

                if (itemsControl.leftBorder == 0 && itemsControl.rightBorder == 0 && itemsControl.stage > 0)
                {
                    itemsControl.stage = 0;
                }
            }

            eventListView.Items.Refresh();
        }

        private void startTrigger_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void endTrigger_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void changeSliderValue(EntityView eventEntity)
        {
            if ((bool)startTrigger.IsChecked) 
            {
                slider.Value = eventEntity.leftBorder;
            }else
            {
                slider.Value = eventEntity.rightBorder;
            }

        }

        private void event_2_Checked(object sender, RoutedEventArgs e)
        {

            setSelectedStatus(1, true);
        }

        private void event_1_Checked(object sender, RoutedEventArgs e)
        {
            setSelectedStatus(0, true);
        }

        private void event_3_Checked(object sender, RoutedEventArgs e)
        {
            setSelectedStatus(2, true);
        }

        private void event_4_Checked(object sender, RoutedEventArgs e)
        {
            setSelectedStatus(3, true);
        }

        private void event_5_Checked(object sender, RoutedEventArgs e)
        {
            setSelectedStatus(4, true);
        }

        private EntityView getSelectedEventEntities() 
        {
            if (isOnlyObserve.IsChecked == true)
            {
                return null;
            }

            return eventEntityList.Find((e) => e.isSelected == true);
        }

        private void setSelectedStatus(int index, bool status)
        {
            try 
            {

                eventEntityList.ForEach((item) => {
                    if (item.id == index)
                    {
                        item.isSelected = true;
                        changeSliderValue(item);
                        item.stage = 2;
                    } else
                    {
                        item.isSelected = false;
                    }
                });

                eventListView.Items.Refresh();
                
            }
            catch(ArgumentOutOfRangeException e)
            {
                Console.WriteLine( e.Message);
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            EntityView selectedEventEntitity = getSelectedEventEntities();
            UpdateEventListView(e.NewValue);
            eventsRelationships.UpdateTable();
            eventsRelationships.DisplayData(viewTable);

            if (selectedEventEntitity == null)
            {
                return;
            }

            if ((bool)startTrigger.IsChecked)
            {
                selectedEventEntitity.leftBorder = e.NewValue;
            }

            if ((bool)endTrigger.IsChecked )
            {
                selectedEventEntitity.rightBorder = e.NewValue;
            }

            Rectangle block = selectedEventEntitity.updateEventBlock(BLOCK_HEIGHT, polygon.ActualWidth);
            Canvas.SetTop(block, selectedEventEntitity.id * BLOCK_HEIGHT);
            polygon.Children.Remove(block);
            polygon.Children.Add(block);
           
        }
       
        private void startTrigger_Click(object sender, RoutedEventArgs e)
        {
            EntityView selectedEntity = getSelectedEventEntities();
            if (selectedEntity != null)
            {
                slider.Value = selectedEntity.leftBorder;
            }
        }

        private void endTrigger_Click(object sender, RoutedEventArgs e)
        {
            EntityView selectedEntity = getSelectedEventEntities();
            if (selectedEntity != null)
            {
                slider.Value = selectedEntity.rightBorder;
            }
        }

        private void isOnlyObserve_Click(object sender, RoutedEventArgs e)
        {
          
            endTrigger.IsEnabled = !endTrigger.IsEnabled;
            startTrigger.IsEnabled = !startTrigger.IsEnabled;
            event_1.IsEnabled = !event_1.IsEnabled;
            event_2.IsEnabled = !event_2.IsEnabled;
            event_3.IsEnabled = !event_3.IsEnabled;
            event_4.IsEnabled = !event_4.IsEnabled;
            event_5.IsEnabled = !event_5.IsEnabled;
        }
    }
}
