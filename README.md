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

参照
----
- [SSL(HTTPS)でファイルのダウンロードができない場合 - [PHP + PHP] ぺんたん info](http://pentan.info/php/ssl_dl_error.html)
- [ファイルダウンロード時のファイル名が文字化けする対処法 - [PHP + PHP] ぺんたん info](http://pentan.info/php/content_disposition_filename.html)
- [久しぶりの技術ネタ。HTTPレスポンスヘッダの[Content-Disposition]について、Safariでの日本語文字化け対策など。 - maachangの日記](http://d.hatena.ne.jp/maachang/20110730/1312008966)

License
-------
under [MIT License](http://opensource.org/licenses/MIT)
