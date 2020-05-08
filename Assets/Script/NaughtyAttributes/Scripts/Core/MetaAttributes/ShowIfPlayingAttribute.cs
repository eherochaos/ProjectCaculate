// ----------------------------------------------------------------------------
// <author>MaZiJun</author>
// <date>29/04/2020</date>
// ----------------------------------------------------------------------------

namespace NaughtyAttributes
{
    using UnityEngine;

    public class ShowIfPlayingAttribute: ShowIfAttributeBase
    {
        public ShowIfPlayingAttribute()
            : base()
        {
            this.Condition = Application.isPlaying;
        }
    }
}