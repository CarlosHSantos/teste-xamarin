using FFImageLoading.Forms;
using Xamarin.Forms;

namespace TheosHero.Controls
{
    public class TheosControl : ScrollView
    {
        const float ParallaxSpeed = 2.25f;

        double _height;

        public TheosControl()
        {
            Scrolled += (sender, e) => Theos();
        }

        public static readonly BindableProperty ParallaxViewProperty =
            BindableProperty.Create(nameof(TheosControl), typeof(CachedImage), typeof(TheosControl), null);

        public View TheosView
        {
            get { return (View)GetValue(ParallaxViewProperty); }
            set { SetValue(ParallaxViewProperty, value); }
        }

        public void Theos()
        {
            if (TheosView == null)
                return;

            if (_height <= 0)
                _height = TheosView.Height;

            var y = -(int)((float)ScrollY / ParallaxSpeed);

            if (y < 0)
                TheosView.TranslationY = y;
            else
                TheosView.TranslationY = 0;
        }
    }
}
