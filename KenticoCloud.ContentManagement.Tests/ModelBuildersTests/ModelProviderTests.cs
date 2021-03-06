﻿using System;
using System.Collections.Generic;
using System.Linq;
using KenticoCloud.ContentManagement.Models.Assets;
using KenticoCloud.ContentManagement.Models.Items;
using KenticoCloud.ContentManagement.Modules.ActionInvoker;
using KenticoCloud.ContentManagement.Modules.ModelBuilders;
using KenticoCloud.ContentManagement.Tests.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace KenticoCloud.ContentManagement.Tests.ModelBuildersTests
{
    public class ModelProviderTests
    {
        private readonly IModelProvider _modelProvider;

        public ModelProviderTests()
        {
            _modelProvider = new ModelProvider();
        }

        [Fact]
        public void GetContentItemVariantModel_ReturnsExpected()
        {
            var expected = GetTestModel();
            var model = new ContentItemVariantModel
            {
                Elements = ToDynamic(expected)
            };
            var actual = _modelProvider.GetContentItemVariantModel<ComplexTestModel>(model).Elements;

            AssertElements(expected, actual);
        }

        [Fact]
        public void GetContentItemVariantUpsertModel_ReturnsExpected()
        {
            var model = GetTestModel();
            var expected = ToDynamic(model);
            var actual = _modelProvider.GetContentItemVariantUpsertModel(model).Elements;

            AssertElements(expected, actual);
        }

        private static void AssertElements(ComplexTestModel expected, ComplexTestModel actual)
        {
            AssertIdentifiers(expected.TeaserImage.Select(x=>x.Id.Value), actual.TeaserImage.Select(x => x.Id.Value));
            Assert.Equal(expected.PostDate, actual.PostDate);
            Assert.Equal(expected.UrlPattern, actual.UrlPattern);
            AssertIdentifiers(expected.RelatedArticles?.Select(x => x.Id.Value), actual.RelatedArticles?.Select(x => x.Id.Value));
            Assert.Equal(expected.BodyCopy, actual.BodyCopy);
            AssertIdentifiers(expected.Personas?.Select(x => x.Id.Value), actual.Personas?.Select(x => x.Id.Value));
            Assert.Equal(expected.Title, actual.Title);
            Assert.Equal(expected.MetaDescription, actual.MetaDescription);
            Assert.Equal(expected.MetaKeywords, actual.MetaKeywords);
            Assert.Equal(expected.Summary, actual.Summary);
        }

        private static ComplexTestModel GetTestModel()
        {
            return new ComplexTestModel
            {
                Title = "text",
                PostDate = DateTime.Now,
                UrlPattern = "urlslug",
                BodyCopy = "RichText",
                TeaserImage = new[] { Guid.NewGuid(), Guid.NewGuid() }.Select(AssetIdentifier.ById).ToArray(),
                RelatedArticles = new[] { Guid.NewGuid(), Guid.NewGuid() }.Select(ContentItemIdentifier.ById).ToArray(),
                Personas = new[] { Guid.NewGuid(), Guid.NewGuid() }.Select(TaxonomyTermIdentifier.ById).ToList(),
            };
        }

        private static dynamic ToDynamic(object value)
        {
            var serialized = JsonConvert.SerializeObject(value);
            return JsonConvert.DeserializeObject<dynamic>(serialized, new JsonSerializerSettings{Converters = new JsonConverter[] {new DynamicObjectJsonConverter()}});
        }

        private static void AssertElements(IDictionary<string, object> expected, IDictionary<string, object> actual)
        {
            Assert.True(JToken.DeepEquals(JToken.FromObject(expected), JToken.FromObject(actual)));
        }

        private static void AssertIdentifiers(IEnumerable<Guid> expected, IEnumerable<Guid> actual)
        {
            if (expected == null && actual == null) return;
            if (expected == null || actual == null)
            {
                Assert.True(false, "Null check");
            }
            Assert.Equal(expected, actual);
        }
    }
}