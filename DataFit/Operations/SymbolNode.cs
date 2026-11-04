//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement

using System.Collections.Generic;

namespace MathLib
{
    public class SymbolNode : Node
    {
        public static Dictionary<string, string> greekSymbols = new Dictionary<string, string>()
        {
            {"alpha",   "\u03b1"},
            {"beta",    "\u03b2"},
            {"gamma",   "\u03b3"},
            {"delta",   "\u03b4"},
            {"epsilon", "\u03b5"},
            {"zeta",    "\u03b6"},
            {"eta",     "\u03b7"},
            {"theta",   "\u03b8"},
            {"iota",   "\u03b9"},
            {"lambda",  "\u03bb"},
            {"mu",      "\u03bc"},
            {"nu",      "\u03bd"},
            {"xi",      "\u03be"},
            {"pi",      "\u03c0"},
            {"PI",      "\u03c0"},
            {"Pi",      "\u03c0"},
            {"rho",     "\u03c1"},
            {"sigma",   "\u03c3"},
            {"tau",     "\u03c4"},
            {"phi",     "\u03c6"},
            {"chi",     "\u03c7"},
            {"psi",     "\u03c8"},
            {"omega",   "\u03c9"}
        };

        public SymbolNode(string symbol)
        {
            if (greekSymbols.ContainsKey(symbol))
            {
                //Replace with greek symbol
                this.symbol = greekSymbols[symbol];
            }
            else
            {
                this.symbol = symbol;
            }
        }
    }
}
