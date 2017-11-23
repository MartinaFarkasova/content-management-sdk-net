﻿using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace KenticoCloud.ContentManagement.Tests
{
    public class ContentManagementClientTests
    {
        private const string PROJECT_ID = "bb6882a0-3088-405c-a6ac-4a0da46810b0";
        private const string API_KEY = "ew0KICAiYWxnIjogIkhTMjU2IiwNCiAgInR5cCI6ICJKV1QiDQp9.ew0KICAidWlkIjogInVzcl8wdkZrVm9rczdyb2prRmR2Slkzc0ZQIiwNCiAgImp0aSI6ICIwYzUyMTYxZGIwZTU0MDU5YjMwZjUwMGUyMTgxYmU1NiIsDQogICJpYXQiOiAiMTUxMTE3ODYwMCIsDQogICJleHAiOiAiMTUxMzc3MDYwMCIsDQogICJwcm9qZWN0X2lkIjogImJiNjg4MmEwMzA4ODQwNWNhNmFjNGEwZGE0NjgxMGIwIiwNCiAgInZlciI6ICIyLjAuMCIsDQogICJwZXJtaXNzaW9ucyI6IFsNCiAgICAidmlldy1jb250ZW50IiwNCiAgICAiY29tbWVudCIsDQogICAgInVwZGF0ZS13b3JrZmxvdyIsDQogICAgInVwZGF0ZS1jb250ZW50IiwNCiAgICAicHVibGlzaCIsDQogICAgImNvbmZpZ3VyZS1zaXRlbWFwIiwNCiAgICAiY29uZmlndXJlLXRheG9ub215IiwNCiAgICAiY29uZmlndXJlLWNvbnRlbnRfdHlwZXMiLA0KICAgICJjb25maWd1cmUtd2lkZ2V0cyIsDQogICAgImNvbmZpZ3VyZS13b3JrZmxvdyIsDQogICAgIm1hbmFnZS1wcm9qZWN0cyIsDQogICAgIm1hbmFnZS11c2VycyIsDQogICAgImNvbmZpZ3VyZS1wcmV2aWV3LXVybCIsDQogICAgImNvbmZpZ3VyZS1jb2RlbmFtZXMiLA0KICAgICJhY2Nlc3MtYXBpLWtleXMiLA0KICAgICJtYW5hZ2UtYXNzZXRzIiwNCiAgICAibWFuYWdlLWxhbmd1YWdlcyIsDQogICAgIm1hbmFnZS13ZWJob29rcyIsDQogICAgIm1hbmFnZS10cmFja2luZyINCiAgXSwNCiAgImF1ZCI6ICJtYW5hZ2Uua2VudGljb2Nsb3VkLmNvbSINCn0.qixIW1NOmyrbfsqoxzHG0NuMMg5yWtG9r0lpb5y31eo";
        private const string EXISTING_ITEM_ID = "a04521af-0a22-4727-94e0-dda78d64384a";
        private const string EXISTING_ITEM_CODENAME = "introduction";
        private const string EXISTING_LANGUAGE_VARIANT_ID = "5f148588-613f-8e32-c023-da82f1308ede";
        private const string EXISTING_LANGUAGE_VARIANT_CODENAME = "Another_language";
        private const string EXISTING_CONTENT_TYPE_CODENAME = "writeapi";
        private const string EXTERNAL_ID = "354136c543gj3154354j1g";

        private static Dictionary<string, object> ELEMENTS = new Dictionary<string, object> { { "name", "Martinko Klingacik44" } };
        private static ContentManagementOptions OPTIONS = new ContentManagementOptions() { ApiKey = API_KEY, ProjectId = PROJECT_ID };


        #region Item Variant

        [Fact]
        public async void UpsertVariantItemIdByLanguageId()
        {
            Thread.Sleep(1000);

            var contentItemVariantUpdateModel = new ContentItemVariantUpdateModel() { Elements = ELEMENTS };
            var identifier = new ContentItemVariantIdentifier() { ItemId = EXISTING_ITEM_ID, LanguageId = EXISTING_LANGUAGE_VARIANT_ID };

            var client = new ContentManagementClient(OPTIONS);
            var responseVariant = await client.UpsertVariantAsync(identifier, contentItemVariantUpdateModel);
            Assert.Equal(EXISTING_ITEM_ID, responseVariant.Item.Id);
        }

        [Fact]
        public async void UpsertVariantByItemCodenameLanguageId()
        {
            Thread.Sleep(1000);

            var contentItemVariantUpdateModel = new ContentItemVariantUpdateModel() { Elements = ELEMENTS };
            var identifier = new ContentItemVariantIdentifier() { ItemCodename = EXISTING_ITEM_CODENAME, LanguageId = EXISTING_LANGUAGE_VARIANT_ID };

            var client = new ContentManagementClient(OPTIONS);
            var responseVariant = await client.UpsertVariantAsync(identifier, contentItemVariantUpdateModel);
            Assert.False(string.IsNullOrEmpty(responseVariant.Item.Id));
        }

        [Fact]
        public async void UpsertVariantByItemIdLanguageCodename()
        {
            Thread.Sleep(1000);

            var contentItemVariantUpdateModel = new ContentItemVariantUpdateModel() { Elements = ELEMENTS };
            var identifier = new ContentItemVariantIdentifier() { ItemId = EXISTING_ITEM_ID, LanguageCodename = EXISTING_LANGUAGE_VARIANT_CODENAME };

            var client = new ContentManagementClient(OPTIONS);
            var responseVariant = await client.UpsertVariantAsync(identifier, contentItemVariantUpdateModel);
            Assert.False(string.IsNullOrEmpty(responseVariant.Item.Id));
        }

        [Fact]
        public async void UpsertVariantByItemCodenameLanguageCodename()
        {
            Thread.Sleep(1000);

            var contentItemVariantUpdateModel = new ContentItemVariantUpdateModel() { Elements = ELEMENTS };
            var identifier = new ContentItemVariantIdentifier() { ItemCodename = EXISTING_ITEM_CODENAME, LanguageCodename = EXISTING_LANGUAGE_VARIANT_CODENAME };

            var client = new ContentManagementClient(OPTIONS);
            var responseVariant = await client.UpsertVariantAsync(identifier, contentItemVariantUpdateModel);
            Assert.False(string.IsNullOrEmpty(responseVariant.Item.Id));
        }

        [Fact]
        public async void ListContentItemVariants()
        {
            Thread.Sleep(1000);

            var client = new ContentManagementClient(OPTIONS);
            var responseVariants = await client.ListContentItemVariantsAsync(EXISTING_ITEM_ID);
            Assert.Equal(EXISTING_ITEM_ID, responseVariants[1].Item.Id);
        }

        [Fact]
        public async void ViewVariantByItemIdLanguageId()
        {
            Thread.Sleep(1000);

            var identifier = new ContentItemVariantIdentifier() { ItemId = EXISTING_ITEM_ID, LanguageId = EXISTING_LANGUAGE_VARIANT_ID };

            var client = new ContentManagementClient(OPTIONS);
            var response = await client.GetContentItemVariantAsync(identifier);
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async void ViewVariantByLanguageCodename()
        {
            Thread.Sleep(1000);

            var identifier = new ContentItemVariantIdentifier() { ItemId = EXISTING_ITEM_ID, LanguageCodename = EXISTING_LANGUAGE_VARIANT_CODENAME };

            var client = new ContentManagementClient(OPTIONS);
            var response = await client.GetContentItemVariantAsync(identifier);
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async void DeleteVariantByLanguageId()
        {
            Thread.Sleep(1000);

            var identifier = new ContentItemVariantIdentifier() { ItemId = "7f2b2c89-074b-471c-b6f4-e94be16c5714", LanguageId = EXISTING_LANGUAGE_VARIANT_ID };

            var client = new ContentManagementClient(OPTIONS);
            var response = await client.DeleteContentItemVariantAsync(identifier);
        }

        [Fact]
        public async void DeleteVariantByLanguageCodename()
        {
            Thread.Sleep(1000);

            var identifier = new ContentItemVariantIdentifier() { ItemId = "7f2b2c89-074b-471c-b6f4-e94be16c5714", LanguageCodename = EXISTING_LANGUAGE_VARIANT_CODENAME };

            var client = new ContentManagementClient(OPTIONS);
            var response = await client.DeleteContentItemVariantAsync(identifier);
        }

        #endregion

        #region Item

        [Fact]
        public async void AddContentItem()
        {
            Thread.Sleep(1000);

            var type = new ManageApiReference() { CodeName = EXISTING_CONTENT_TYPE_CODENAME };
            var item = new ContentItemPostModel() { Name = "Hooray!", Type = type };

            var client = new ContentManagementClient(OPTIONS);
            var responseItem = await client.AddContentItemAsync(item);
            Assert.Equal("Hooray!", responseItem.Name);
        }

        [Fact]
        public async void ListContentItems()
        {
            Thread.Sleep(1000);

            var client = new ContentManagementClient(OPTIONS);
            var response = await client.ListContentItemsAsync();
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async void UpdateContentItemById()
        {
            Thread.Sleep(1000);

            var identifier = new ContentItemIdentifier() { ItemId = EXISTING_ITEM_ID };
            var sitemapLocation = new List<ManageApiReference>();
            var item = new ContentItemPutModel() { Name = "test42", SitemapLocations = sitemapLocation};

            var client = new ContentManagementClient(OPTIONS);
            var contentItemReponse = await client.UpdateContentItemAsync(identifier, item);
            Assert.Equal("test42", contentItemReponse.Name);
        }

        [Fact]
        public async void ViewContentItemById()
        {
            Thread.Sleep(1000);
            var identifier = new ContentItemIdentifier() { ItemId = EXISTING_ITEM_ID };

            var client = new ContentManagementClient(OPTIONS);
            var contentItemReponse = await client.GetContentItemAsync(identifier);
            Assert.Equal(EXISTING_ITEM_ID, contentItemReponse.Id.ToString());
        }

        [Fact]
        public async void DeleteContentItemById()
        {
            Thread.Sleep(1000);

            var identifier = new ContentItemIdentifier() { ItemId = "existingID" };

            var client = new ContentManagementClient(OPTIONS);
            var response = await client.DeleteContentItemAsync(identifier);
            Assert.True(response.IsSuccessStatusCode);
        }

        #endregion
    }
}