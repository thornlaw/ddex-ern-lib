using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Schema;
using Xml.Schema.Linq;

namespace DDEX.ERN37.MessagingClassLibrary
{
    public static class XTypedElementExtensions
    {
        public static ValidationResult Validate(this XTypedElement element, string nameSpace, string schemaUrl)
        {
            var result = new ValidationResult();

            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add(nameSpace, schemaUrl);

            var doc = new XDocument(element.Untyped);
            doc.Validate(schemas, (o, e) =>
            {
                result.Messages.Add(e.Message);
            });

            return result;
        }
    }
}
