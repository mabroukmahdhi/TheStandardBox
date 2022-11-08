// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;

namespace TheStandardBox.UIKit.Blazor.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class FieldAttribute : Attribute
    {
        public FieldAttribute()
        { }

        public FieldAttribute(string name) =>
            this.Name = name; 

        public string Name { get; } 
    }
}