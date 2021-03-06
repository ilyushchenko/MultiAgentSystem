﻿using System.Windows.Media;

namespace MultiAgentSystem.UI.ViewModels
{
    public class DrawnObjectViewModel : BaseViewModel
    {
        private int _x;
        private int _y;
        private ImageSource _sprite;

        public DrawnObjectViewModel(ImageSource source)
        {
            Sprite = source;
        }

        public ImageSource Sprite
        {
            get => _sprite;
            set
            {
                _sprite = value;
                OnPropertyChanged();
            }
        }

        public int X
        {
            get => _x;
            set
            {
                _x = value;
                OnPropertyChanged();
            }
        }

        public int Y
        {
            get => _y;
            set
            {
                _y = value;
                OnPropertyChanged();
            }
        }
    }
}
