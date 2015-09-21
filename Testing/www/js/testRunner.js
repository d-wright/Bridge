﻿/* global Bridge */

"use strict";

Bridge.define('ClientTestLibrary.RunTests', {
    statics: {
        config: {
            init: function () {
                Bridge.ready(this.main);
            }
        },
        main: function () {
            QUnit.module("Inheritance, abstract, virtual and overloading");
            QUnit.test("Overloading static methods", ClientTestLibrary.TestOverloadStaticMethods.testStatic);
            QUnit.test("Overloading instance methods", ClientTestLibrary.TestOverloadInstanceMethods.testInstance);
            QUnit.test("Inheritance A instance", ClientTestLibrary.TestInheritance.testA);
            QUnit.test("Inheritance B instance", ClientTestLibrary.TestInheritance.testB);
            QUnit.test("Inheritance B instance as A type", ClientTestLibrary.TestInheritance.testAB);
            QUnit.test("Abstract B instance", ClientTestLibrary.TestAbstractClass.testB);
            QUnit.test("Abstract C instance", ClientTestLibrary.TestAbstractClass.testC);
            QUnit.test("Abstract BC instance as A type", ClientTestLibrary.TestAbstractClass.testBC);
            QUnit.test("Virtual methods", ClientTestLibrary.TestVirtualMethods.testB);

            QUnit.module("Reference types");
            QUnit.test("Instance constructors and methods", ClientTestLibrary.TestReferenceTypes.testInstanceConstructorsAndMethods);
            QUnit.test("Static constructors and methods", ClientTestLibrary.TestReferenceTypes.testStaticConstructorsAndMethods);
            QUnit.test("Method parameters", ClientTestLibrary.TestReferenceTypes.testMethodParameters);

            QUnit.module("Value types");
            QUnit.test("Instance constructors and methods", ClientTestLibrary.TestValueTypes.testInstanceConstructorsAndMethods);
            QUnit.test("Static constructors and methods", ClientTestLibrary.TestValueTypes.testStaticConstructorsAndMethods);

            QUnit.module("Interfaces");
            QUnit.test("Interface method and property", ClientTestLibrary.TestInterfaces.testInterfaceMethodAndProperty);
            QUnit.test("Explicit interface", ClientTestLibrary.TestInterfaces.testExplicitInterfaceMethodAndProperty);
            QUnit.test("Simple two interfaces", ClientTestLibrary.TestInterfaces.testTwoInterfaces);

            QUnit.module("Method parameters");
            QUnit.test("Default and params", ClientTestLibrary.TestMethodParametersClass.test);

            QUnit.module("String");
            QUnit.test("Common", ClientTestLibrary.TestStringFunctions.strings);
            QUnit.test("String Enumerable", ClientTestLibrary.TestStringFunctions.enumerable);
            QUnit.test("String #393", ClientTestLibrary.TestStringFunctions.issueBridge393);
            QUnit.test("StringBuilder", ClientTestLibrary.TestStringBuilderFunctions.stringBuilders);

            QUnit.module("Date and time");
            QUnit.test("Common", ClientTestLibrary.TestDateFunctions.dateTimes);
            QUnit.test("#329", ClientTestLibrary.TestDateFunctions.bridge329);
            QUnit.test("#349", ClientTestLibrary.TestDateFunctions.bridge349);

            QUnit.module("Try/Catch");
            QUnit.test("Try/Catch simpe", ClientTestLibrary.TestTryCatchBlocks.simpleTryCatch);
            QUnit.test("Try/Catch caught exceptions", ClientTestLibrary.TestTryCatchBlocks.caughtExceptions);
            QUnit.test("Try/Catch thrown exceptions", ClientTestLibrary.TestTryCatchBlocks.thrownExceptions);
            QUnit.test("#320", ClientTestLibrary.TestTryCatchBlocks.bridge320);
            QUnit.test("#343", ClientTestLibrary.TestTryCatchBlocks.bridge343);
            QUnit.test("Try/Catch/Finally simple", ClientTestLibrary.TestTryCatchFinallyBlocks.simpleTryCatchFinally);
            QUnit.test("Try/Catch/Finally caught exceptions", ClientTestLibrary.TestTryCatchFinallyBlocks.caughtExceptions);
            QUnit.test("Try/Catch/Finally thrown exceptions", ClientTestLibrary.TestTryCatchFinallyBlocks.thrownExceptions);

            QUnit.module("System");
            QUnit.test("Version TestConstructors", ClientTestLibrary.TestVersion.testConstructors);
            QUnit.test("Version Equals and GetHashCode", ClientTestLibrary.TestVersion.testEqualsGetHashCode);
            QUnit.test("Version ToString", ClientTestLibrary.TestVersion.testToString);
            QUnit.test("Version Parse", ClientTestLibrary.TestVersion.testParse);
            QUnit.test("Version Operators", ClientTestLibrary.TestVersion.testOperators);

            QUnit.module("Issues");
            QUnit.test("#169", ClientTestLibrary.TestBridgeIssues.n169);
            QUnit.test("#240", ClientTestLibrary.TestBridgeIssues.n240);
            QUnit.test("#264", ClientTestLibrary.TestBridgeIssues.n264);
            QUnit.test("#266", ClientTestLibrary.TestBridgeIssues.n266);
            QUnit.test("#272", ClientTestLibrary.TestBridgeIssues.n272);
            QUnit.test("#273", ClientTestLibrary.TestBridgeIssues.n273);
            QUnit.test("#277", ClientTestLibrary.TestBridgeIssues.n277);
            QUnit.test("#294", ClientTestLibrary.TestBridgeIssues.n294);
            QUnit.test("#304", ClientTestLibrary.TestBridgeIssues.n304);
            QUnit.test("#305", ClientTestLibrary.TestBridgeIssues.n305);
            QUnit.test("#306", ClientTestLibrary.TestBridgeIssues.n306);
            QUnit.test("#335", ClientTestLibrary.TestBridgeIssues.n335);
            QUnit.test("#336", ClientTestLibrary.TestBridgeIssues.n336);
            QUnit.test("#337", ClientTestLibrary.TestBridgeIssues.n337);
            QUnit.test("#338", ClientTestLibrary.TestBridgeIssues.n338);
            QUnit.test("#339", ClientTestLibrary.TestBridgeIssues.n339);
            QUnit.test("#340", ClientTestLibrary.TestBridgeIssues.n340);
            QUnit.test("#341", ClientTestLibrary.TestBridgeIssues.n341);
            QUnit.test("#342", ClientTestLibrary.TestBridgeIssues.n342);
            QUnit.test("#377", ClientTestLibrary.TestBridgeIssues.n377);
            QUnit.test("#383", ClientTestLibrary.TestBridgeIssues.n383);
            QUnit.test("#395", ClientTestLibrary.TestBridgeIssues.n395);
            QUnit.test("#406", ClientTestLibrary.TestBridgeIssues.n406);
            QUnit.test("#407", ClientTestLibrary.TestBridgeIssues.n407);
            QUnit.test("#409", ClientTestLibrary.TestBridgeIssues.n409);
            QUnit.test("#410", ClientTestLibrary.TestBridgeIssues.n410);
            QUnit.test("#418", ClientTestLibrary.TestBridgeIssues.n418);
            QUnit.test("#422", ClientTestLibrary.TestBridgeIssues.n422);
            QUnit.test("#428", ClientTestLibrary.TestBridgeIssues.n428);
            QUnit.test("#435", ClientTestLibrary.TestBridgeIssues.n435);
            QUnit.test("#436", ClientTestLibrary.TestBridgeIssues.n436);
            QUnit.test("#438", ClientTestLibrary.TestBridgeIssues.n438);
            QUnit.test("#439", ClientTestLibrary.TestBridgeIssues.n439);
            QUnit.test("#442", ClientTestLibrary.TestBridgeIssues.n442);
            QUnit.test("#460", ClientTestLibrary.TestBridgeIssues.n460);
            QUnit.test("#467", ClientTestLibrary.TestBridgeIssues.n467);
            QUnit.test("#469", ClientTestLibrary.TestBridgeIssues.n469);
            QUnit.test("#470", ClientTestLibrary.TestBridgeIssues.n470);
            QUnit.test("#472", ClientTestLibrary.Bridge472.test);
            QUnit.test("#485", ClientTestLibrary.Bridge485.testUseCase);

            QUnit.module("LINQ");
            QUnit.test("Aggregate operators", ClientTestLibrary.Linq.TestLinqAggregateOperators.test);
            QUnit.test("Aggregate operators", ClientTestLibrary.Linq.TestLinqAggregateOperators.bridge315);
            QUnit.test("Conversion operators", ClientTestLibrary.Linq.TestLinqConversionOperators.test);
            QUnit.test("Element operators", ClientTestLibrary.Linq.TestLinqElementOperators.test);
            QUnit.test("Generation operators", ClientTestLibrary.Linq.TestLinqGenerationOperators.test);
            QUnit.test("Join operators", ClientTestLibrary.Linq.TestLinqJoinOperators.test);
            QUnit.test("Grouping operators", ClientTestLibrary.Linq.TestLinqGroupingOperators.test);
            QUnit.test("Complex grouping operators.", ClientTestLibrary.Linq.TestLinqGroupingOperators.testComplexGrouping);
            QUnit.test("Anagram grouping operators.", ClientTestLibrary.Linq.TestLinqGroupingOperators.testAnagrams);
            QUnit.test("Miscellaneous operators", ClientTestLibrary.Linq.TestLinqMiscellaneousOperators.test);
            QUnit.test("Ordering operators", ClientTestLibrary.Linq.TestLinqOrderingOperators.test);
            QUnit.test("Partitioning operators", ClientTestLibrary.Linq.TestLinqPartitioningOperators.test);
            QUnit.test("Projection operators", ClientTestLibrary.Linq.TestLinqProjectionOperators.test);
            QUnit.test("Quantifiers", ClientTestLibrary.Linq.TestLinqQuantifiers.test);
            QUnit.test("Query execution", ClientTestLibrary.Linq.TestLinqQueryExecution.test);
            QUnit.test("Restriction operators", ClientTestLibrary.Linq.TestLinqRestrictionOperators.test);
            QUnit.test("Set operators", ClientTestLibrary.Linq.TestLinqSetOperators.test);
        }
    }
});

