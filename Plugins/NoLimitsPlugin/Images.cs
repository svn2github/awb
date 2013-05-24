﻿/*

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation; either version 2 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
*/

using System.Collections.Generic;

using WikiFunctions.Lists.Providers;
using WikiFunctions.Plugin;

namespace WikiFunctions.Plugins.ListMaker.NoLimitsPlugin
{
    /// <summary>
    /// 
    /// </summary>
    public class ImageFileLinksNoLimitsListProvider : ImageFileLinksListProvider, IListMakerPlugin
    {
        public ImageFileLinksNoLimitsListProvider()
        {
            Limit = 1000000;
        }

        public override List<Article> MakeList(params string[] searchCriteria)
        {
            return Base.CanUsePlugin() ? base.MakeList(searchCriteria) : null;
        }

        public override string DisplayText
        {
            get { return base.DisplayText + " (NL, Admin & Bot)"; }
        }

        public string Name
        {
            get { return "ImageFileLinksNoLimitsListProvider"; }
        }
    }
}
