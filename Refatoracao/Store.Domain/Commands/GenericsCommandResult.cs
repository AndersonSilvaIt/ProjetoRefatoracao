using Store.Domain.Commands.Interfaces;

namespace Store.Domain.Commands {

	public class GenericsCommandResult : ICommandResult {

		public GenericsCommandResult(bool success, string message, object data) {
			Success = success;
			Message = message;
			Data = data;
		}

		public bool Success { get; set; }
		public string Message { get; set; }
		public object Data { get; set; }
	}
}
