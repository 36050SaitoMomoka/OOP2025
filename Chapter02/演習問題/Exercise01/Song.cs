using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Exercise01 {

    //2.1.1 【クラスを定義】
    public class Song {
        public string Title { get; private set; } = string.Empty;
        public string ArtistName { get; private set; } = string.Empty;
        public int Length { get; private set; }

        //2.1.2 【コンストラクタ】
        public Song(string Title, string ArtistName, int Length) {
            this.Title = Title;
            this.ArtistName = ArtistName;
            this.Length = Length;
        }
    }
}
