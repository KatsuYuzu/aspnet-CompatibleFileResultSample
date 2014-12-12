aspnet-CompatibleFileResultSample
=================================

ASP.NET MVC の FileResult をレガシーブラウザでダウンロードするときのサンプルです。
IE8 以下のいくつかの不具合を回避しています。

- [CompatibleFileResultAttribute.cs](/src/WebApplication/Controllers/CompatibleFileResultAttribute.cs)

```csharp
// ####################
// # Download will fail when the ssl and no-cache.
// ####################

var cacheControl = response.Headers["Cache-Control"];
if (!string.IsNullOrEmpty(cacheControl))
{
    response.Headers["Cache-Control"] = RemoveNoCache(cacheControl);
}

var pragma = response.Headers["Pragma"];
if (!string.IsNullOrEmpty(cacheControl))
{
    response.Headers["Pragma"] = RemoveNoCache(pragma);
}
```

```csharp
// ####################
// # Not supported RFC 6266 (RFC 2231/RFC 5987).
// ####################

var contentDisposition = response.Headers["Content-Disposition"];
if (!string.IsNullOrEmpty(contentDisposition))
{
    response.Headers["Content-Disposition"] = ReplaceFileName(contentDisposition);
}
```

- [HomeController.cs](/src/WebApplication/Controllers/HomeController.cs)

```csharp
[CompatibleFileResultAttribute]
public ActionResult File(string id)
```

詳細はこちらへ
- [commit ActionResult のレガシーブラウザ互換対応](https://github.com/KatsuYuzu/aspnet-CompatibleFileResultSample/commit/07e218edfb7d6ed59459b11f55494ac86d03f594)
- [ASP.NET MVC の ActionFilter でレガシー IE でのファイルダウンロードの文字化け、不具合と戦う #aspnetjp - KatsuYuzuのブログ](http://katsuyuzu.hatenablog.jp/entry/2014/12/12/131754)

License
-------
under [MIT License](http://opensource.org/licenses/MIT)
