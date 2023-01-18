using FTS.Base.Business;
using System.Collections.Generic;

namespace FTS.Base.Model
{
    public class Dm_TemplateManagerObject : ManagerObjectInfoBase
    {
        /// <summary>
        /// Dm_TemplateObject
        /// </summary>
        public Dm_TemplateObject dmTemplate;

        /// <summary>
        /// Dm_Template_DetailObject
        /// </summary>
        public List<Dm_Template_DetailObject> dmTemplateDetail;

        /// <summary>
        /// prkeydetail: prKey của dmTemplateDetailObject
        /// </summary>
        public decimal PrKeyDetail = 0;

        /// <summary>
        /// fieldname : field điều chỉnh dữ liệu
        /// </summary>
        public string FieldName = string.Empty;

        /// <summary>
        /// Dm_TemplateManagerObject
        /// </summary>
        public Dm_TemplateManagerObject()
        {
            this.dmTemplate = new Dm_TemplateObject();
            this.dmTemplateDetail = new List<Dm_Template_DetailObject>();
        }

        /// <summary>
        /// AddNewDetailObject
        /// </summary>
        /// <returns></returns>
        public ObjectInfoBase AddNewDetailObject()
        {
            Dm_Template_DetailObject dmTemplateDetailObject = new Dm_Template_DetailObject();
            this.dmTemplateDetail.Add(dmTemplateDetailObject);
            return dmTemplateDetailObject;
        }

        /// <summary>
        /// FindDetailObject
        /// </summary>
        /// <param name="prkey"></param>
        /// <returns></returns>
        public Dm_Template_DetailObject FindDetailObject(decimal prkey)
        {
            foreach (Dm_Template_DetailObject dmTemplateDetailObject in this.dmTemplateDetail)
            {
                if (dmTemplateDetailObject.PR_KEY == prkey)
                {
                    return dmTemplateDetailObject;
                }
            }
            return null;
        }
    }
}
