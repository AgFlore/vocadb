﻿using System;
using VocaDb.Model.Helpers;

namespace VocaDb.Model.Domain {

	/// <summary>
	/// Date object without time or timezone.
	/// Dates are considered equal as long as day, month and year are the same, even if timezones were different.
	/// </summary>
	/// <remarks>
	/// For example, normally midnight 2015/03/09 00:00 in Japan time would be 2015/03/08 in the US because of timezones.
	/// Obviously 2015/03/09 00:00 in Japan time and US time would be different dates, but here we want to handle them the same.
	/// </remarks>
	public struct Date {

		public static implicit operator Date(DateTime? dateTime) {
			return new Date(dateTime);
		}

		public static implicit operator DateTime?(Date? date) {
			return date.HasValue ? date.Value.DateTime : null;
		}

		private DateTime? dateTime;

		public Date(DateTime? dateTime) : this() {
			DateTime = dateTime;
		}

		public Date(DateTimeOffset? dateTimeOffset) : this() {
			DateTime = dateTimeOffset.HasValue ? (DateTime?)dateTimeOffset.Value.Date : null;
		}

		public DateTime? DateTime {
			get { return dateTime; }
			set { dateTime = value != null ? (DateTime?)System.DateTime.SpecifyKind(value.Value, DateTimeKind.Utc).Date : null; }
		}

		public bool Equals(DateTime? anotherDateTime) {
			
			return DateTimeHelper.DateEquals(DateTime, anotherDateTime);

		}

		public bool Equals(Date? another) {
			
			if (another == null)
				return false;

			return DateTimeHelper.DateEquals(DateTime, another.Value.DateTime);

		}

		public bool Equals(Date another) {
			
			return DateTimeHelper.DateEquals(DateTime, another.DateTime);

		}

		public override bool Equals(object obj) {
					
			if (obj is DateTime?)
				return Equals((DateTime?)obj);

			if (obj is Date?)
				return Equals((Date?)obj);

			return false;

		}

		public override int GetHashCode() {
			return DateTime.HasValue ? DateTime.Value.GetHashCode() : base.GetHashCode();
		}

		public override string ToString() {
			return DateTime.HasValue ? DateTime.Value.ToString() : base.ToString();
		}

	}

}
