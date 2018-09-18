/*
This file is part of the iText (R) project.
Copyright (c) 1998-2018 iText Group NV
Authors: iText Software.

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License version 3
as published by the Free Software Foundation with the addition of the
following permission added to Section 15 as permitted in Section 7(a):
FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
ITEXT GROUP. ITEXT GROUP DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
OF THIRD PARTY RIGHTS

This program is distributed in the hope that it will be useful, but
WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
or FITNESS FOR A PARTICULAR PURPOSE.
See the GNU Affero General Public License for more details.
You should have received a copy of the GNU Affero General Public License
along with this program; if not, see http://www.gnu.org/licenses or write to
the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
Boston, MA, 02110-1301 USA, or download the license from the following URL:
http://itextpdf.com/terms-of-use/

The interactive user interfaces in modified source and object code versions
of this program must display Appropriate Legal Notices, as required under
Section 5 of the GNU Affero General Public License.

In accordance with Section 7(b) of the GNU Affero General Public License,
a covered work must retain the producer line in every PDF that is created
or manipulated using iText.

You can be released from the requirements of the license by purchasing
a commercial license. Buying such a license is mandatory as soon as you
develop commercial activities involving the iText software without
disclosing the source code of your own applications.
These activities include: offering paid services to customers as an ASP,
serving PDFs on the fly in a web application, shipping iText with a closed
source product.

For more information, please contact iText Software Corp. at this
address: sales@itextpdf.com
*/
using System;
using System.Collections.Generic;
using iText.StyledXmlParser.Node;
using iText.StyledXmlParser.Node.Impl.Jsoup;
using iText.Svg.Processors.Impl;
using iText.Svg.Renderers;
using iText.Test;

namespace iText.Svg.Css {
    public class SvgStyleResolverIntegrationTest : SvgIntegrationTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/svg/css/SvgStyleResolver/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/svg/css/SvgStyleResolver/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            ITextTest.CreateDestinationFolder(destinationFolder);
        }

        [NUnit.Framework.Test]
        public virtual void RedCirleTest() {
            String svg = "<svg\n" + "   width=\"210mm\"\n" + "   height=\"297mm\"\n" + "   viewBox=\"0 0 210 297\"\n" 
                + "   version=\"1.1\"\n" + "  <title id=\"title4508\">Red Circle</title>\n" + "    <ellipse\n" + "       id=\"path3699\"\n"
                 + "       cx=\"96.005951\"\n" + "       cy=\"110.65774\"\n" + "       rx=\"53.672619\"\n" + "       ry=\"53.294643\"\n"
                 + "       style=\"stroke-width:1.76388889;stroke:#da0000;stroke-opacity:1;fill:none;stroke-miterlimit:4;stroke-dasharray:none\" />\n"
                 + "</svg>\n";
            JsoupXmlParser xmlParser = new JsoupXmlParser();
            IDocumentNode root = xmlParser.Parse(svg);
            IBranchSvgNodeRenderer nodeRenderer = (IBranchSvgNodeRenderer)new DefaultSvgProcessor().Process(root).GetRootRenderer
                ();
            IDictionary<String, String> actual = new Dictionary<String, String>();
            //Traverse to ellipse
            ISvgNodeRenderer ellipse = nodeRenderer.GetChildren()[0];
            actual.Put("stroke", ellipse.GetAttribute("stroke"));
            actual.Put("stroke-width", ellipse.GetAttribute("stroke-width"));
            actual.Put("stroke-opacity", ellipse.GetAttribute("stroke-opacity"));
            IDictionary<String, String> expected = new Dictionary<String, String>();
            expected.Put("stroke-width", "1.76388889");
            expected.Put("stroke", "#da0000");
            expected.Put("stroke-opacity", "1");
            NUnit.Framework.Assert.AreEqual(expected, actual);
        }

        [NUnit.Framework.Test]
        public virtual void StyleTagProcessingTest() {
            String svg = "<svg\n" + "   width=\"210mm\"\n" + "   height=\"297mm\"\n" + "   viewBox=\"0 0 210 297\"\n" 
                + "   version=\"1.1\"\n" + "   id=\"svg8\"\n" + "   >\n" + "  <style>\n" + "\tellipse{\n" + "\t\tstroke-width:1.76388889;\n"
                 + "\t\tstroke:#da0000;\n" + "\t\tstroke-opacity:1;\n" + "\t}\n" + "  </style>\n" + "    <ellipse\n" +
                 "       id=\"path3699\"\n" + "       cx=\"96.005951\"\n" + "       cy=\"110.65774\"\n" + "       rx=\"53.672619\"\n"
                 + "       ry=\"53.294643\"\n" + "       style=\"fill:none;stroke-miterlimit:4;stroke-dasharray:none\" />\n"
                 + "</svg>\n";
            JsoupXmlParser xmlParser = new JsoupXmlParser();
            IDocumentNode root = xmlParser.Parse(svg);
            IBranchSvgNodeRenderer nodeRenderer = (IBranchSvgNodeRenderer)new DefaultSvgProcessor().Process(root).GetRootRenderer
                ();
            IDictionary<String, String> actual = new Dictionary<String, String>();
            //Traverse to ellipse
            ISvgNodeRenderer ellipse = nodeRenderer.GetChildren()[0];
            actual.Put("stroke", ellipse.GetAttribute("stroke"));
            actual.Put("stroke-width", ellipse.GetAttribute("stroke-width"));
            actual.Put("stroke-opacity", ellipse.GetAttribute("stroke-opacity"));
            IDictionary<String, String> expected = new Dictionary<String, String>();
            expected.Put("stroke-width", "1.76388889");
            expected.Put("stroke", "#da0000");
            expected.Put("stroke-opacity", "1");
            NUnit.Framework.Assert.AreEqual(expected, actual);
        }
    }
}