using System.Collections.Generic;
using System.Text;

namespace AML.Models.Role;

public class RoleClass
{
    public string name { get; private set; }
    public List<Attribute> attributes { get; private set; }

    public List<RoleClass> subRoleClassesList;

    public RoleClass(string name, List<Attribute> attributes)
    {
        this.name = name;
        this.attributes = attributes;
    }
    

    public override string ToString()
    {
        var attrListString = new StringBuilder();
        foreach (var attr in attributes)
        {
            attrListString.Append(attr.ToString());
        }
        return $"[" +
               $"name={name} " +
               $"attributes=[{attrListString.ToString()}]" +
               $"]";
    }
}