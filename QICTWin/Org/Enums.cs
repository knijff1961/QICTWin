// <copyright file="Enums.cs" company="Fouroaks">
// Copyright (c) 2007, 2008 All Right Reserved, http://www.fouroaks.nl/
//
// This source is subject to the Microsoft Permissive License.
// Please see the License.txt file for more information.
// All other rights reserved.
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// </copyright>
// <author>Peter Knijff</author>
// <email>peter.knijff@fouroaks.nl</email>
// <date>2006-2010</date>
//
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QICTWin
{
    enum  E_LOG : ulong
    {
        E_LOG_MESSAGES1_________________ = 0x0000000000000001,
        E_LOG_MESSAGES2_________________ = 0x0000000000000002,
        E_LOG_MESSAGES3_________________ = 0x0000000000000004,
        E_PREPARE_______________________ = 0x0000000000000008,

        E_PREP_PARAMETER_VALUES_________ = 0x0000000000000010,
        E_PREP_INTERNAL_PARAMETER_VALUES = 0x0000000000000020,
        E_PREP_DISPLAY_ALL_PAIRS________ = 0x0000000000000040,
        E_PREP_DISPLAY_ALL_PAIRS2_______ = 0x0000000000000080,

        E_PREP_PARAMETER_POSITIONS______ = 0x0000000000000100,
        E_PREP_UNUSED_COUNTS____________ = 0x0000000000000200,
        E_CORE_BEST_PAIR________________ = 0x0000000000000400,
        E_CORE_BEST_PAIR_POSITION_______ = 0x0000000000000800,

        E_CORE_BEST_PAIR_ORDER__________ = 0x0000000000001000,
        E_CORE_PLACED_PARAMS____________ = 0x0000000000002000,
        E_CORE_POSSIBLE_VALUES__________ = 0x0000000000004000,
        E_CORE_EXAMINE_PAIRS____________ = 0x0000000000008000,

        E_CORE_BEST_VALUE_______________ = 0x0000000000010000,
        E_CORE_ADD_CANDIDATE____________ = 0x0000000000020000,
        E_CORE_ALL_CANDIDATE_TESTSETS___ = 0x0000000000030000,
        E_CORE_CANDIDATE_NUMBER_________ = 0x0000000000040000,

        E_CORE_UPDATE_STEPS_____________ = 0x0000000000100000,
        E_FINAL_________________________ = 0x0000000000200000,
        E_PREP_UNUSED_PAIRS_SEARCH______ = 0x0000000000400000,


        E_CONSOLE_______________________ = 0x8000000000000000,
    }
}
