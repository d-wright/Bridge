﻿using Bridge.Contract;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using Attribute = ICSharpCode.NRefactory.CSharp.Attribute;

namespace Bridge.Translator
{
    public class TypeInfo : ITypeInfo
    {
        public TypeInfo()
        {
            this.StaticMethods = new Dictionary<string, List<MethodDeclaration>>();
            this.InstanceMethods = new Dictionary<string, List<MethodDeclaration>>();
            this.StaticProperties = new Dictionary<string, List<EntityDeclaration>>();
            this.InstanceProperties = new Dictionary<string, List<EntityDeclaration>>();
            this.FieldsDeclarations = new Dictionary<string, FieldDeclaration>();
            this.EventsDeclarations = new Dictionary<string, EventDeclaration>();
            this.Dependencies = new List<IPluginDependency>();
            this.Ctors = new List<ConstructorDeclaration>();
            this.Operators = new Dictionary<OperatorType, List<OperatorDeclaration>>();
            this.StaticConfig = new TypeConfigInfo();
            this.InstanceConfig = new TypeConfigInfo();
        }

        public string Key
        {
            get;
            set;
        }

        public TypeConfigInfo StaticConfig
        {
            get;
            set;
        }

        public TypeConfigInfo InstanceConfig
        {
            get;
            set;
        }

        public Dictionary<OperatorType, List<OperatorDeclaration>> Operators
        {
            get;
            protected set;
        }

        public Dictionary<string, EventDeclaration> EventsDeclarations
        {
            get;
            set;
        }

        public TypeDeclaration TypeDeclaration
        {
            get;
            set;
        }

        public bool IsStatic
        {
            get;
            set;
        }

        public ClassType ClassType
        {
            get;
            set;
        }

        public string Namespace
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public HashSet<string> Usings
        {
            get;
            set;
        }

        public List<ConstructorDeclaration> Ctors
        {
            get;
            set;
        }

        public ConstructorDeclaration StaticCtor
        {
            get;
            set;
        }

        public Dictionary<string, FieldDeclaration> FieldsDeclarations
        {
            get;
            protected set;
        }

        public Dictionary<string, List<MethodDeclaration>> StaticMethods
        {
            get;
            protected set;
        }

        public Dictionary<string, List<MethodDeclaration>> InstanceMethods
        {
            get;
            protected set;
        }

        public Dictionary<string, List<EntityDeclaration>> StaticProperties
        {
            get;
            protected set;
        }

        public Dictionary<string, List<EntityDeclaration>> InstanceProperties
        {
            get;
            protected set;
        }

        public bool HasStatic
        {
            get
            {
                return this.StaticConfig.HasMembers
                       || this.StaticMethods.Count > 0
                       || this.StaticProperties.Count > 0
                       || this.StaticCtor != null
                       || this.Operators.Count > 0;
            }
        }

        public bool HasRealStatic(IEmitter emitter)
        {
            var result = this.StaticConfig.HasMembers
                       || this.StaticProperties.Count > 0
                       || this.StaticCtor != null
                       || this.Operators.Count > 0;

            if (result)
            {
                return true;
            }

            if (this.StaticMethods.Any(group =>
            {
                foreach (var method in group.Value)
                {
                    if (method.Attributes.Count == 0)
                    {
                        return true;
                    }

                    foreach (var attrSection in method.Attributes)
                    {
                        foreach (var attr in attrSection.Attributes)
                        {
                            var rr = emitter.Resolver.ResolveNode(attr.Type, emitter);
                            if (rr.Type.FullName == "Bridge.InitAttribute")
                            {
                                if (!attr.HasArgumentList)
                                {
                                    return true;
                                }

                                var expr = attr.Arguments.First();

                                var argrr = emitter.Resolver.ResolveNode(expr, emitter);
                                if (argrr.ConstantValue is int)
                                {
                                    var value = (int)argrr.ConstantValue;

                                    if (value == 1 || value == 2)
                                    {
                                        return false;
                                    }
                                }

                                return true;
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                }

                return false;
            }))
            {
                return true;
            }

            return false;
        }

        public bool HasInstantiable
        {
            get
            {
                return this.InstanceConfig.HasMembers
                       || this.InstanceMethods.Count > 0
                       || this.InstanceProperties.Count > 0
                       || this.Ctors.Count > 0;
            }
        }

        private int enumValue = -1;

        public virtual int LastEnumValue
        {
            get
            {
                return this.enumValue;
            }
            set
            {
                this.enumValue = value;
            }
        }

        public bool IsEnum
        {
            get;
            set;
        }

        public string Module
        {
            get;
            set;
        }

        public List<IPluginDependency> Dependencies
        {
            get;
            set;
        }

        public ITypeInfo ParentType
        {
            get;
            set;
        }

        public bool IsObjectLiteral
        {
            get;
            set;
        }


        public string FileName
        {
            get;
            set;
        }

        public IType Type
        {
            get;
            set;
        }
    }
}
