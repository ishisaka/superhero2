using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SuperHero2.Models;

namespace SuperHero2.Controllers
{
    /// <summary>
    /// SuperHero2 WEB API
    /// </summary>
    public class HeroesController : ApiController
    {
        /// <summary>
        /// ヒーローのリストを取得する。
        /// </summary>
        /// <remarks>
        /// ODataのインターフェイスをサポートするように戻り値をIQueryable(T)型に変更した。
        /// </remarks>
        /// <returns>ヒーローのリスト(JSON).</returns>
        public IQueryable<Hero> Get() {
            List<Hero> heroes;
            using (var context = new HeroContext()) {
                heroes = context.Heroes.ToList();
            }
            //List<T>をIQueryable<T>に変換する。
            return heroes.AsQueryable();
        
        }
        
        /// <summary>
        /// ヒーローを取得する
        /// </summary>
        /// <param name="id">取得したいヒーローのid</param>
        /// <exception cref="HttpResponseException">
        /// 指定されたidのヒーローが存在しない場合は404を返します。
        /// </exception>
        /// <returns>ヒーロー</returns>
        public Hero Get(int id) {
            Hero _hero;
            using (var context = new HeroContext()) {
                _hero = context.Heroes.Find(id);
                if (_hero == null) {
                    //idのヒーローが見つからない場合には404を返す。
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
            }
            return _hero;
        }

        /// <summary>
        /// ヒーローを追加する
        /// </summary>
        /// <param name="heroName">追加するヒーローの名前</param>
        /// <remarks>
        /// Fiddler2などでのテストの仕方
        /// httpヘッダに追加
        ///   Accept: text/html
        ///   Content-Type: text/json; charset=utf-8
        /// ボディに追加するヒーローの名前をutf-8でダブルクォート(")で囲う。
        ///   例:"仮面ライダー1号"
        /// </remarks>
        /// <returns>追加に成功した場合201を返して、追加したヒーローを返す。</returns>
        public HttpResponseMessage Post([FromBody]string heroName) {
            Hero _hero;
            using (var context = new HeroContext()) {
                _hero = new Hero();
                _hero.Name = heroName;
                context.Heroes.Add(_hero);
                context.SaveChanges();
                //201でレスポンスメッセージを作成する。
                return Request.CreateResponse<Hero>(HttpStatusCode.Created, _hero);
            }
        }


        //
        //JSON(もしくはxml)で追加を受け付ける場合
        //
        //public HttpResponseMessage Post([FromBody]Hero _hero) {
        //    using (var context = new HeroContext()) {
        //        context.Heroes.Add(_hero);
        //        context.SaveChanges();
        //        //201でレスポンスメッセージを作成する。
        //        return Request.CreateResponse<Hero>(HttpStatusCode.Created, _hero);
        //    }
        //}
        
        /// <summary>
        /// ヒーローの名前を変更する
        /// </summary>
        /// <param name="id">名前を変更したいヒーローのid</param>
        /// <param name="name">変更する名前</param>
        /// <remarks>
        /// Fiddler2などでのテストの仕方
        /// URL
        ///   http://(hostname):(port)/api/heroes?id=(変更するid)
        /// httpヘッダに追加
        ///   Accept: text/html
        ///   Content-Type: text/json; charset=utf-8
        /// ボディに追加するヒーローの名前をutf-8でダブルクォート(")で囲う。
        ///   例:"仮面ライダー1号"
        /// </remarks>
        /// <returns>
        /// 変更に成功した場合には200を返して、変更結果を返す。
        /// </returns>
        public HttpResponseMessage Put(int id, [FromBody]string name) {
            using (var context = new HeroContext()) {
                var _hero = context.Heroes.Find(id);
                if (_hero != null) {
                    _hero.Name = name;
                    context.SaveChanges();
                    return Request.CreateResponse<Hero>(HttpStatusCode.OK, _hero);
                }
                else {
                    //idのヒーローが見つからない場合には404を返す。
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
            }
        }

        /// <summary>
        /// 引数で与えられたヒーローを削除する。
        /// </summary>
        /// <exception cref="HttpResponseException">
        /// 引数idで与えられたヒーローが存在しない場合には404エラーが発生する。
        /// </exception>
        /// <param name="id"></param>
        public void Delete(int id) {
            Hero _hero;
            using (var context = new HeroContext()) {
                _hero = context.Heroes.Find(id);
                if (_hero != null) {
                    context.Heroes.Remove(_hero);
                    context.SaveChanges();
                }
                else {
                    //idのヒーローが見つからない場合には404を返す。
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
            }
        }
    }
}