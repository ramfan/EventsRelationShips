using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Lab1
{
    class EntityView : EventEntity
    {
        private static readonly int LIMIT = 1;
        public Color color { set; get; } = Colors.Black;
        public Rectangle block { get; } = new Rectangle();
        public TextBlock textBlock { get; } = new TextBlock();

        public EntityView(int id, String name, Boolean isSelected) : base(id, name, isSelected)
        {

        }

        public EntityView(int id, String name, Color color) : base(id, name)
        {
            textBlock.Text = name;
            textBlock.Width = 50;
            this.color = color;
            block.Fill = new SolidColorBrush(color);
            block.Stroke = new SolidColorBrush(Colors.Black);
        }

        public Rectangle updateEventBlock(int height, double polygoneWidth)
        {
            double countPixelsPerPercent = polygoneWidth * 0.01d;

            if (leftBorder + LIMIT > rightBorder)
            {
                return block;
            }

            block.Width = (rightBorder - leftBorder) * countPixelsPerPercent;

            if (leftBorder > 0)
            {
                Canvas.SetLeft(block, leftBorder * countPixelsPerPercent);
            }

            block.Height = height;
            return block;
        }
    }
}