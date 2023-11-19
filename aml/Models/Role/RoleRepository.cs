using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Aml.Engine.CAEX;
using Aml.Engine.CAEX.Extensions;

namespace AML.Models.Role;

public class RoleRepository
{
    private readonly CAEXDocument file;
    private List<RoleClassLib> roleClassLibsList = new List<RoleClassLib>();

    public RoleRepository(CAEXDocument file)
    {
        this.file = file;
        parse();
    }

    // ReSharper disable once InconsistentNaming
    private void getRoleClass(CAEXWrapper roleClass)
    {
        var roleClassChildrenList = new List<RoleClass>();
        Console.WriteLine($"Start class: {roleClass.Name()}");
        foreach (var child in roleClass.CAEXChildren("RoleClass"))
        {
            Console.WriteLine(child.Name());
            // roleClassChildrenList.Add(new RoleClass(
            //         name: child.Name(),
            //         attributes: AttrList(child.Node),
            //         subRoleClassesList: getRoleClass(child)
            //     )
            // );
        }
        // return new RoleClass(roleClass.Name(), AttrList(roleClass.Node), roleClassChildrenList);
        // return roleClassChildrenList;
    }

    private List<Attribute> AttrList(RoleFamilyType role)
    {
        var attrList = new List<Attribute>();
        foreach (var roleAttr in role.Attribute.Elements)
        {
            attrList.Add(new Attribute(roleAttr:roleAttr));
        }

        return attrList;
    }

    private void parse()
    {
        foreach (var rcl in file.CAEXFile.RoleClassLib)
        {
            var roleClassList = new List<RoleClass>();

            foreach (var role in rcl.RoleClass)
            {
                
                roleClassList.Add(new RoleClass(name: role.Name, attributes: AttrList(role)));

                // foreach (var subroles in role.RoleClass.Elements)
                // {
                //     Console.WriteLine(subroles.Name);
                // }
            }

            roleClassLibsList.Add(new RoleClassLib(
                name: rcl.Name(),
                version: rcl.Version,
                rolesList: roleClassList)
            );
        }
    }


    public List<RoleClassLib> getRoles()
    {
        return roleClassLibsList;
    }

    public string getRolesNames()
    {
        var stringBuilder = new StringBuilder();
        foreach (var el in roleClassLibsList)
        {
            stringBuilder.Append(el.ToString());
        }

        return stringBuilder.ToString();
    }
}