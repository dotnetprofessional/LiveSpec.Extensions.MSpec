using System;

namespace LiveSpec.Extensions.MSpec
{
    public class Background : Step<BackgroundAttribute>{
        public Background(Type me) : base(me)
        {
        }
    }
}