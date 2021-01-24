﻿using System.Collections.Generic;
using MockApi.Contracts;
using MockApi.Model;
using MockApi.Services;
using Xunit;

namespace MockApi.Tests
{
    public class ResponseCacheTests
    {
        private readonly IResponseCache _responseCache;

        public ResponseCacheTests()
        {
            _responseCache = new ResponseCache();
        }

        [Fact]
        public void CalculateKey_WhenExecuted_ReturnsMethodPlusPath()
        {
            var key = _responseCache.CalculateKey("method", "path");

            Assert.Equal("methodpath", key);
        }

        [Fact]
        public void ContainsResponse_WhenResponseDoesNotExist_ReturnsFalse()
        {
            var result = _responseCache.ContainsResponse("key");

            Assert.False(result);
        }

        [Fact]
        public void ContainsResponse_WhenResponseExists_ReturnsTrue()
        {
            _responseCache.SetResponse("key", new VirtualResponse());

            var result = _responseCache.ContainsResponse("key");

            Assert.True(result);
        }

        [Fact]
        public void GetResponse_WhenResponseDoesNotExist_ThrowsException()
        {
            Assert.Throws<KeyNotFoundException>(() => _responseCache.GetResponse("key"));
        }

        [Fact]
        public void GetResponse_WhenResponseExists_ReturnsCachedResponse()
        {
            var response = new VirtualResponse();
            _responseCache.SetResponse("key", response);

            var result = _responseCache.GetResponse("key");

            Assert.Equal(response, result);
        }
    }
}