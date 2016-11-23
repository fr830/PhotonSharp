﻿using System.Collections.Generic;
using System;

namespace Photon
{
    internal class Node
    {
        public Node Parent;

        public virtual IEnumerable<Node> Child()
        {
            yield break;
        }

        internal virtual void Compile(CompileParameter param)
        {
            throw new Exception("'Compile' not implement: " + this.ToString());
        }

        internal virtual void Resolve(CompileParameter param)
        {

        }

        public void BuildRelation( )
        {
            foreach( var c in Child() )
            {
                c.Parent = this;
            }
        }
    }


    internal class Stmt : Node
    {

    }

    internal class BadStmt : Stmt
    {

    }

    internal class Expr : Node
    {

    }

    internal class BadExpr : Expr
    {

    }




   






    


   
}
