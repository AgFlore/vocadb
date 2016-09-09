﻿using System;
using VocaDb.Model.Domain.Globalization;
using VocaDb.Model.DataContracts.Songs;

namespace VocaDb.Model.Domain.Songs {

	public class LyricsForSong : IEquatable<LyricsForSong> {

		private string cultureCode;
		private string notes;
		private Song song;
		private string source;
		private string value;

		public LyricsForSong() {}

		public LyricsForSong(Song song, string val, string source, TranslationType translationType, string cultureCode) {

			Song = song;
			Source = source;
			TranslationType = translationType;
			CultureCode = cultureCode;
			Value = val;

		}

		public virtual string CultureCode {
			get { return cultureCode; }
			set {
				ParamIs.NotNull(() => value);
				cultureCode = value;
			}
		}

		public virtual int Id { get; protected set; }

		public virtual string Notes {
			get { return notes; }
			set {
				ParamIs.NotNull(() => value);				
				notes = value;
			}
		}

		public virtual Song Song {
			get { return song; }
			set {
				ParamIs.NotNull(() => value);
				song = value;
			}
		}

		public virtual string Source {
			get { return source; }
			set {
				ParamIs.NotNull(() => value);
				source = value;
			}
		}

		public virtual TranslationType TranslationType { get; set; }

		public virtual string Value {
			get { return value; }
			set {
				ParamIs.NotNull(() => value);
				this.value = value;
			}
		}

		public virtual bool ContentEquals(LyricsForSongContract contract) {

			if (contract == null)
				return false;

			return (TranslationType == contract.TranslationType 
				&& CultureCode == contract.CultureCode 
				&& Source == contract.Source 
				&& Value == contract.Value);

		}

		public virtual bool Equals(LyricsForSong another) {

			if (another == null)
				return false;

			if (ReferenceEquals(this, another))
				return true;

			if (Id == 0)
				return false;

			return this.Id == another.Id;

		}

		public override bool Equals(object obj) {
			return Equals(obj as LyricsForSong);
		}

		public override int GetHashCode() {
			return Id.GetHashCode();
		}

	}

}
