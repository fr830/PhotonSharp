﻿using System;
using Photon.AST;
using Photon.OpCode;
using Photon.Scanner;


namespace Photon.Compiler
{
    public partial class Compiler
    {

        bool CompileDeclare(CommandSet cm, Node n, bool lhs)
        {
            if (n is FuncDeclare)
            {
                var v = n as FuncDeclare;

                var newset = new CommandSet(v.Name.Name);

                var funcIndex = _exe.AddCmdSet(newset);

                var c = new FuncValue(funcIndex);
                var ci = _exe.Constants.Add(c);

                cm.Add(new Command(Opcode.LoadC, ci)).Comment = c.GetDesc();
                cm.Add(new Command(Opcode.SetR, v.Name.ScopeInfo.Slot)).Comment = v.Name.Name;

                CompileNode(newset, v.Body, false);

                return true;
            }

            if (n is VarDeclare)
            {
                var v = n as VarDeclare;

                CompileNode(cm, v.Values[0], false);
                CompileNode(cm, v.Names[0], true);
                return true;
            }

            return false;
        }
    }
}