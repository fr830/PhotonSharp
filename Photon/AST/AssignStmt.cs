﻿using Photon.OpCode;
using System.Collections.Generic;

namespace Photon.AST
{
    public class AssignStmt : Stmt
    {
        public List<Expr> LHS;
        public List<Expr> RHS;
        public AssignStmt(List<Expr> lhs, List<Expr> rhs)
        {
            LHS = lhs;
            RHS = rhs;
        }

        public AssignStmt(Expr lhs, Expr rhs)
        {
            LHS = new List<Expr>();
            LHS.Add(lhs);

            RHS = new List<Expr>();
            RHS.Add(rhs);
        }

        public override IEnumerable<Node> Child()
        {
            foreach (var e in LHS)
            {
                yield return e;
            }


            foreach (var e in RHS)
            {
                yield return e;
            }
        }

        public override string ToString()
        {
            return "AssignStmt";
        }

        public override void Compile(Executable exe, CommandSet cm, bool lhs)
        {
            foreach (var e in RHS)
            {
                e.Compile(exe, cm, false );
            }

            foreach (var e in LHS)
            {
                e.Compile(exe, cm, true );
            }
        }
    }

}