//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Configuration Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Design.Validation;

namespace Microsoft.Practices.EnterpriseLibrary.Configuration.Design
{
    /// <summary>
    /// <para>Represents a storage provider for the meta-configuration of a specific section of configuration for an application.</para>
    /// </summary>
    [Image(typeof(TransformerNode))]
    public class XmlSerializerTransformerNode : TransformerNode
    {
        private XmlSerializerTransformerData xmlSerializerTransformerData;

        /// <summary>
        /// <para>Initalize a new instance of the <see cref="XmlSerializerTransformerNode"/> class.</para>
        /// </summary>
        public XmlSerializerTransformerNode() : this(new XmlSerializerTransformerData(SR.XmlSerializerTransformerNodeFriendlyName))
        {
        }

        /// <summary>
        /// <para>Initalize a new instance of the <see cref="XmlSerializerTransformerNode"/> class with a display name and <see cref="XmlSerializerTransformerData"/> object.</para>
        /// </summary>        
        /// <param name="xmlSerializerTransformerData">
        /// <para>The runtime configuration data.</para>
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="xmlSerializerTransformerData"/> is a <see langword="null"/> reference (Nothing in Visual Basic).</para>
        /// </exception>
        public XmlSerializerTransformerNode(XmlSerializerTransformerData xmlSerializerTransformerData) : base(xmlSerializerTransformerData)
        {
            this.xmlSerializerTransformerData = xmlSerializerTransformerData;
        }

        /// <summary>
        /// <para>Gets or sets the fully qualified name of a class that implements the <see cref="ITransformer"/> interface.</para>
        /// </summary>
        /// <value>
        /// <para>The fully qualified name of a class that implements the <see cref="ITransformer"/> interface.</para>
        /// </value>
        [Required]
        [SRCategory(SR.Keys.CategoryGeneral)]
        [SRDescription(SR.Keys.TypeNameDescription)]
        [ReadOnly(true)]
        public string TypeName
        {
            get { return xmlSerializerTransformerData.TypeName; }
            set { xmlSerializerTransformerData.TypeName = value; }
        }

        /// <summary>
        /// <para>Gets the <see cref="TransformerData"/> object that this node represents.</para>
        /// </summary>
        /// <returns>
        /// <para>The <see cref="TransformerData"/> object that this node represents.</para>
        /// </returns>        
        [Browsable(false)]
        public override TransformerData TransformerData
        {
            get

            {
                XmlSerializerTransformerData data = base.TransformerData as XmlSerializerTransformerData;
                if (data == null)
                {
                    return base.TransformerData;
                }
                data.XmlIncludeTypes.Clear();
                Type[] includeTypes = null;
                if (this.Parent != null)
                {
                    includeTypes = XmlIncludeTypeService.GetXmlIncludeTypes(Site, this.Parent.Name, Hierarchy);
                }
                if (includeTypes != null)
                {
                    foreach (Type includeType in includeTypes)
                    {
                        XmlIncludeTypeData includeTypeData = new XmlIncludeTypeData(includeType.Name, includeType.AssemblyQualifiedName);
                        if (!data.XmlIncludeTypes.Contains(includeTypeData.Name))
                        {
                            data.XmlIncludeTypes.Add(includeTypeData);
                        }
                    }
                }

                foreach (XmlIncludeTypeNode typeNode in Nodes)
                {
                    if (!data.XmlIncludeTypes.Contains(typeNode.XmlIncludeTypeData.Name))
                    {
                        data.XmlIncludeTypes.Add(typeNode.XmlIncludeTypeData);
                    }
                }
                return data;
            }
        }

        /// <summary>
        /// <para>Adds the <see cref="XmlIncludeTypeNode"/> objects for this node if any exist.</para>
        /// </summary>
        protected override void OnSited()
        {
            base.OnSited();
            if (xmlSerializerTransformerData.XmlIncludeTypes != null)
            {
                Nodes.Clear();
                foreach (XmlIncludeTypeData xmlIncludeTypeData in this.xmlSerializerTransformerData.XmlIncludeTypes)
                {
                    Nodes.Add(new XmlIncludeTypeNode(xmlIncludeTypeData));
                }
            }
        }

		/// <summary>
		/// <para>Adds the base commands and an <see cref="AddChildNodeCommand"/>, a command for creating <see cref="XmlIncludeTypeNode"/> objects and if the parent node is not readonly, a <see cref="RemoveNodeCommand"/> to the menus for the user interface.</para>
		/// </summary>
        protected override void OnAddMenuItems()
        {
            base.OnAddMenuItems();
            ConfigurationMenuItem item = new ConfigurationMenuItem(SR.XmlIncludeTypeMenuItem,
                new AddChildNodeCommand(Site, typeof(XmlIncludeTypeNode)), 
                this, 
                                                                   Shortcut.None,
                SR.GenericCreateStatusText(SR.XmlIncludeTypeMenuItem),
                InsertionPoint.New);
            AddMenuItem(item);
        }
    }
}