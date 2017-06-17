﻿// Adopted, originally created as part of GraphSharp library
// This code is distributed under Microsoft Public License 
// (for details please see \docs\Ms-PL)

using System.Collections.Generic;
using System.Windows;
using AssemblyVisualizer.Controls.Graph.QuickGraph;

namespace AssemblyVisualizer.Controls.Graph.GraphSharp.Layout
{
    public class CompoundLayoutContext<TVertex, TEdge, TGraph> 
        : LayoutContext<TVertex, TEdge, TGraph>, ICompoundLayoutContext<TVertex, TEdge, TGraph>
        where TEdge : IEdge<TVertex>
        where TGraph : class, IBidirectionalGraph<TVertex, TEdge>
    {
        public CompoundLayoutContext(
            TGraph graph,
            IDictionary<TVertex, Point> positions,
            IDictionary<TVertex, Size> sizes,
            global::AssemblyVisualizer.Controls.Graph.GraphSharp.Layout.LayoutMode mode,
            IDictionary<TVertex, Thickness> vertexBorders,
            IDictionary<TVertex, CompoundVertexInnerLayoutType> layoutTypes)
            : base( graph, positions, sizes, mode )
        {
            VertexBorders = vertexBorders;
            LayoutTypes = layoutTypes;
        }

        public IDictionary<TVertex, Thickness> VertexBorders { get; private set; }
        public IDictionary<TVertex, CompoundVertexInnerLayoutType> LayoutTypes { get; private set; }
    }
}