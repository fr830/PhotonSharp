﻿
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Photon
{

    public class Executable
    {

        List<Package> _packages = new List<Package>();
       
        // 外部代理函数
        Dictionary<string, ValueDelegate> _delegateByName = new Dictionary<string, ValueDelegate>();


        internal Package AddPackage( string name, Scope top )
        {
            var pkg = new Package( _packages.Count,  name, this, top);

            _packages.Add(pkg);

            return pkg;

        }

        internal Package GetPackageByName(string name)
        {
            foreach (var pkg in _packages)
            {
                if (pkg.Name == name)
                {
                    return pkg;
                }
            }

            return null;
        }

        internal Procedure GetProcedure( int packageID, int cmdSetID )
        {            
            var pkg = _packages[packageID];
            if (pkg == null)
                return null;

            return pkg.GetProcedure(cmdSetID);
        }

        internal bool FindCmdSetInPackage(string name, out Procedure outP, out Package outPkg)
        {
            outP = null;
            outPkg = null;

            foreach( var pkg in _packages )
            {
                outP = pkg.FindProcedureByName(name);
                if (outP != null)
                {
                    outPkg = pkg;
                    return true;
                }
            }
            

            return false;
        }

        internal void AddDelegate( string name, ValueDelegate d )
        {
            _delegateByName.Add(name, d);
        }

        public Package GetPackage(int pkgid)
        {
            return _packages[pkgid];
        }

        public int PackageCount
        {
            get { return _packages.Count; }
        }

        public SourceFile GetSourceFile(string filename)
        {
            foreach (var pkg in _packages)
            {
                foreach (var f in pkg.FileList)
                {
                    if (f.Source.Name == filename)
                    {
                        return f.Source;
                    }
                }
            }

            return null;
        }


        public void RegisterPackage(Type classType)
        {
            var obj = classType.GetCustomAttributes(typeof(DelegateAttribute), false);

            
        }

        public void DebugPrint()
        {
            // 语法树
            foreach (var pkg in _packages)
            {
                Debug.WriteLine(string.Format("============= {0} =============", pkg.Name));
                pkg.DebugPrint();
            }

            Debug.WriteLine("");
        }
    }
}
