using System;
using System.Collections.Generic;
using System.Text;

namespace EFSamples.FinanceModel
{
	public abstract class Transaction
	{
		public Int64 Id { get; set; }
		public DateTime Date { get; set; }
		public decimal Value { get; set; }
		public decimal Fee { get; set; }
		public string Note { get; set; }
		public Int64 AccountId { get; set; }
		public Account Account { get; set; }
	}

	public class CashTransaction : Transaction
	{
		// Cash In will have a positive Value; Cash Out will have negative.
		// Do we need to differentiate between Deposit and Transfer In?
		// Deposit seems to imply that the cash comes from the User.
		// Could we have an explicit source to show the difference?
		// Types:
		// - Deposit: from the User (some other account, maybe not tracked here)
		// - Dividend: from a Security
		// - Interest: from the account institution
		// - InLieu?: from a Security for M&A, reverse split (so not a Dividend)
	}

	public class SecurityTransaction : Transaction
	{
		// Positive = In and Negative = Out???
		public decimal Quantity { get; set; }
		// the security
		// Types: same issue as with CashXact; use same solution.
		// - Buy:
		//   - Quantity should be positive because we are adding to the account.
		//   - Value should be negative because we are removing cash from the account.
		//     - This feels wrong, possibly because of what "value" means.
		//     - We are not changing the value of the account, because the security
		//     - has the same value as the cash. Instead, we are changing the amount
		//     - of cash in the account.
		//     - Should we then have an explicit "CashChange" property?
		//       - This also feels wrong.
		// - Transfer In:
		//   - Quantity should be positive because we are adding to the account.
		//   - Value should be 0 because we are not changing the cash in the account.
		//     - But how then do we get the price per share? Or is this irrelevent?
		// - Stock Split:
		//   - Quantity should be positive because we are adding to the account.
		//   - Value = 0???
		// - Reverse Stock Split:
		//   - Quantity should be negative because we are removing from the account?
		//     - Technically this is correct, but it feels wrong.
		//     - Otherwise would need to do something like use two transactions,
		//       one that removes all of the existing shares and one that adds
		//       the new shares.
		//       - Would both transactions be shown as if they were independent?
		//       - What if the user tries to delete one of them? Deletes them both?
		// - Stock Dividend:
		//   - Quantity would be positive because we are adding to the account.
		//   - Value = 0???
		//   - So same as TransferIn but with a different source.
		// - Sell:
		//   - Quantity should be negative because we are removing from the account.
		//   - Value should be positive because we are adding cash to the account?
	}
}
