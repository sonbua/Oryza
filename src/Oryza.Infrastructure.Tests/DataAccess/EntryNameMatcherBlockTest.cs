using System.Collections;
using System.Collections.Generic;
using Oryza.Entities;
using Oryza.Infrastructure.DataAccess;
using Oryza.TestBase;
using Oryza.TestBase.Composition;
using Xunit;

namespace Oryza.Infrastructure.Tests.DataAccess
{
    public class EntryNameMatcherBlockTest : Test
    {
        [Theory]
        [ClassData(typeof (EntryTypeList))]
        public void GivenAnEntryNameAndAListOfEntryTypes_ReturnsExpectedResult(EntriesMatching entriesMatching, bool expected)
        {
            // arrange
            var entryNameMatcherBlock = _serviceProvider.GetService<EntryNameMatcherBlock>();

            // act
            var match = entryNameMatcherBlock.Handle(entriesMatching);

            // assert
            Assert.Equal(expected, match.IsMatched);
        }

        private class EntryTypeList : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                             {
                                 new EntriesMatching
                                 {
                                     EntryName = "Thailand 100% B grade",
                                     ExistingEntryTypes = new List<EntryType>()
                                 },
                                 false
                             };

                yield return new object[]
                             {
                                 new EntriesMatching
                                 {
                                     EntryName = "Thailand   100%   B   grade",
                                     ExistingEntryTypes = new List<EntryType> {new EntryType {Name = "Thailand100percentBGrade", NameVariants = new List<string> {"Thailand 100% B grade"}}}
                                 },
                                 true
                             };

                yield return new object[]
                             {
                                 new EntriesMatching
                                 {
                                     EntryName = "THAILAND   100%   B   GRADE",
                                     ExistingEntryTypes = new List<EntryType> {new EntryType {Name = "Thailand100percentBGrade", NameVariants = new List<string> {"Thailand 100% B grade"}}}
                                 },
                                 true
                             };

                yield return new object[]
                             {
                                 new EntriesMatching
                                 {
                                     EntryName = "XXX",
                                     ExistingEntryTypes = new List<EntryType> {new EntryType {Name = "Thailand100percentBGrade", NameVariants = new List<string> {"Thailand 100% B grade"}}}
                                 },
                                 false
                             };
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}