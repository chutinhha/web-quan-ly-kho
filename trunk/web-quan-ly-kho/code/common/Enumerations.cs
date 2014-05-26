
namespace QLCV.code.common
{
	/// <summary>
	/// Enum to define event status.
	/// </summary>
	public enum EventStatus
	{
		/// <summary>
		/// Tao moi ban dau.
		/// </summary>
		Active = 1,
		/// <summary>
		/// Da chap nhan tham gia su kien.
		/// </summary>
		Approved = 2,
		/// <summary>
		/// Tu choi tham gia su kien.
		/// </summary>
		Reject = 3,
		/// <summary>
		/// Huy tham gia su kien
		/// </summary>
		Cancel = 4,
        /// <summary>
        /// Su kien goi dt cho khach hang
        /// </summary>
        Call = 5
	}
}
