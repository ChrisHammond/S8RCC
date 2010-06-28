/*
' Copyright (c) 2004-2010 Shift8Read
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System.Collections.Generic;
//using System.Xml;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Search;

namespace Shift8Read.Dnn.CommunityCreditSubmit
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for s8rCommunityCredit
    /// </summary>
    /// -----------------------------------------------------------------------------
    public class FeatureController : IPortable, ISearchable, IUpgradeable
    {

        #region Public Methods



        #endregion

        #region Optional Interfaces

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// ExportModule implements the IPortable ExportModule Interface
        /// </summary>
        /// <param name="ModuleID">The Id of the module to be exported</param>
        /// -----------------------------------------------------------------------------
        public string ExportModule(int ModuleID)
        {
            //string strXML = "";

            //List<s8rCommunityCreditInfo> cols8rCommunityCredits = Gets8rCommunityCredits(ModuleID);
            //if (cols8rCommunityCredits.Count != 0)
            //{
            //    strXML += "<s8rCommunityCredits>";

            //    foreach (s8rCommunityCreditInfo objs8rCommunityCredit in cols8rCommunityCredits)
            //    {
            //        strXML += "<s8rCommunityCredit>";
            //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objs8rCommunityCredit.Content) + "</content>";
            //        strXML += "</s8rCommunityCredit>";
            //    }
            //    strXML += "</s8rCommunityCredits>";
            //}

            //return strXML;

            throw new System.NotImplementedException("The method or operation is not implemented.");
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// ImportModule implements the IPortable ImportModule Interface
        /// </summary>
        /// <param name="ModuleID">The Id of the module to be imported</param>
        /// <param name="Content">The content to be imported</param>
        /// <param name="Version">The version of the module to be imported</param>
        /// <param name="UserId">The Id of the user performing the import</param>
        /// -----------------------------------------------------------------------------
        public void ImportModule(int ModuleID, string Content, string Version, int UserID)
        {
            //XmlNode xmls8rCommunityCredits = DotNetNuke.Common.Globals.GetContent(Content, "s8rCommunityCredits");
            //foreach (XmlNode xmls8rCommunityCredit in xmls8rCommunityCredits.SelectNodes("s8rCommunityCredit"))
            //{
            //    s8rCommunityCreditInfo objs8rCommunityCredit = new s8rCommunityCreditInfo();
            //    objs8rCommunityCredit.ModuleId = ModuleID;
            //    objs8rCommunityCredit.Content = xmls8rCommunityCredit.SelectSingleNode("content").InnerText;
            //    objs8rCommunityCredit.CreatedByUser = UserID;
            //    Adds8rCommunityCredit(objs8rCommunityCredit);
            //}

            throw new System.NotImplementedException("The method or operation is not implemented.");
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// GetSearchItems implements the ISearchable Interface
        /// </summary>
        /// <param name="ModInfo">The ModuleInfo for the module to be Indexed</param>
        /// -----------------------------------------------------------------------------
        public DotNetNuke.Services.Search.SearchItemInfoCollection GetSearchItems(DotNetNuke.Entities.Modules.ModuleInfo ModInfo)
        {
            //SearchItemInfoCollection SearchItemCollection = new SearchItemInfoCollection();

            //List<s8rCommunityCreditInfo> cols8rCommunityCredits = Gets8rCommunityCredits(ModInfo.ModuleID);

            //foreach (s8rCommunityCreditInfo objs8rCommunityCredit in cols8rCommunityCredits)
            //{
            //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objs8rCommunityCredit.Content, objs8rCommunityCredit.CreatedByUser, objs8rCommunityCredit.CreatedDate, ModInfo.ModuleID, objs8rCommunityCredit.ItemId.ToString(), objs8rCommunityCredit.Content, "ItemId=" + objs8rCommunityCredit.ItemId.ToString());
            //    SearchItemCollection.Add(SearchItem);
            //}

            //return SearchItemCollection;

            throw new System.NotImplementedException("The method or operation is not implemented.");
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// UpgradeModule implements the IUpgradeable Interface
        /// </summary>
        /// <param name="Version">The current version of the module</param>
        /// -----------------------------------------------------------------------------
        public string UpgradeModule(string Version)
        {
            throw new System.NotImplementedException("The method or operation is not implemented.");
        }

        #endregion

    }

}
