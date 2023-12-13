using System;
using System.Collections.Generic;
using System.Linq;
using Aml.Engine.CAEX;
using Aml.Engine.CAEX.Extensions;
using AML.Models.InternalElement;
using AML.Models.Role;
using Attribute = AML.Models.Attribute;

namespace AML;

class Hello
{
    public static void Main()
    {
        // WriteLine("Current Dir:" + Directory.GetCurrentDirectory());
        var file = CAEXDocument.LoadFromFile("../../../../TestAML.aml");
        // var elements = document.CAEXFile.InstanceHierarchy;

        // foreach (var el in elements)
        // {
        //     WriteLine(el.CAEXChild("InternalElement").CAEXChild("InternalElement").CAEXChild("ExternalInterface").CAEXChild("ExternalInterface").Name());
        // }


        // foreach (var instanceHierarchy in document.CAEXFile.InstanceHierarchy)
        // {
        //     WriteLine($"1{instanceHierarchy}: ");
        //     foreach (var internalElement in instanceHierarchy.CAEXChildren("InternalElement"))
        //     {
        //         WriteLine("\tIE: "+internalElement.Name() + ":");
        //         foreach (var ei in internalElement.Descendants<ExternalInterfaceType>())
        //         {
        //             WriteLine("\t\tEI: " + ei.Name);
        //         }
        //         foreach (var rc in internalElement.Descendants<RoleClassType>())
        //         {
        //             WriteLine("\t\tRC: " + rc.Name);
        //         }
        //     }
        // }
        // var element = file.CAEXFile.RoleClassLib[0];
        // var attr = element.RoleClass[0].Attribute.Elements;
        //
        //
        // foreach (var a in attr)
        // {
        //     WriteLine($"{a.Attribute("Name").Value} = {a.Value}");
        // }
        // List<RoleClassLib> roleClassLibsList = new List<RoleClassLib>();
        //
        // foreach (var rcl in file.CAEXFile.RoleClassLib)
        // {
        //     var roleClassList = new List<RoleClass>();
        //
        //     foreach (var role in rcl.RoleClass)
        //     {
        //         var attrList = new List<Attribute>();
        //         foreach (var roleAttr in role.Attribute.Elements)
        //         {
        //             attrList.Add(new Attribute(roleAttr));
        //         }
        //
        //         roleClassList.Add(new RoleClass(name: role.Name, attributes: attrList));
        //     }
        //     roleClassLibsList.Add(new RoleClassLib(
        //         name: rcl.Name(),
        //         version: rcl.Version,
        //         rolesList: roleClassList)
        //     );
        // }
        //
        // foreach (var rcll in roleClassLibsList)
        // {
        //     Console.WriteLine(rcll.ToString());
        // }

        // var roleRepository = new RoleRepository(file);
        // Console.WriteLine(roleRepository.getRolesNames());

        foreach (var instanceHierarchy in file.CAEXFile.InstanceHierarchy)
        {
            var ieList = getChildren(instanceHierarchy);
            foreach (var ie in ieList)
            {
                Console.WriteLine(ie.ToString());
            }
        }
    }

    private static List<Attribute> getAttrs(CAEXWrapper caex)
    {
        var attrsList = new List<Attribute>();
        
        foreach (var attrs in caex.Descendants<AttributeType>())
        // foreach (var attrs in caex.CAEXChildren("Attribute"))
        {
            // Console.WriteLine($"{attrs}");
            // Console.WriteLine($"{caex} -> {attrs.Name()}={attrs.Value}");
            foreach (var attrName in caex.CAEXChildren("Attribute"))
            {
                if (attrName.Name() == attrs.Name())
                {
                    // Console.WriteLine($"{caex} -> {attrs.Name()}={attrs.Value}");
                    attrsList.Add(new Attribute(attrs.Name(), attrs.Value));
                }
                
            }
        }
        // caex.TagName;
        return attrsList;
    }

    private static List<InternalElement> getChildren(CAEXWrapper caex)
    {
        var ieList = new List<InternalElement>();
        foreach (var i in caex.CAEXChildren("InternalElement"))
        {
            // Console.WriteLine($"{caex} -> {i}");
            ieList.Add(new InternalElement(i.Name(), getChildren(i),getAttrs(i)));
        }

        return ieList;
    }
}