using System.Xml.Linq;

namespace AML.Models;

public class Attribute
{
    public string name { get; private set; }
    public string value { get; private set; }

    public Attribute(string name, string value)
    {
        this.name = name;
        this.value = value;
    }

    public Attribute(XElement roleAttr)
    {
        name = roleAttr.Attribute("Name").Value;
        value = roleAttr.Value;
    }

    public override string ToString()
    {
        return $"[" +
               $"name={name} " +
               $"value={value}" +
               $"]";
    }
}