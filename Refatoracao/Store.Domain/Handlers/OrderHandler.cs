using Flunt.Notifications;
using Store.Domain.Commands;
using Store.Domain.Commands.Interfaces;
using Store.Domain.Entities;
using Store.Domain.Repositories.Interfaces;

namespace Store.Domain.Handlers {
	public class OrderHandler: Notifiable,
		IHandler<CreateOrderCommand> {

		private readonly ICustomerRepository _customerRepository;
		private readonly IDeliveryFeeRepository _deliveryFeeRepository;
		private readonly IDiscountRepository _discountRepository;
		private readonly IProductRepository _produtoRepository;
		private readonly IOrderRepository _orderRepository;

		public OrderHandler(ICustomerRepository customerRepository, 
							IDeliveryFeeRepository deliveryFeeRepository, 
							IDiscountRepository discountRepository, 
							IProductRepository produtoRepository, 
							IOrderRepository orderRepository) {

			_customerRepository = customerRepository;
			_deliveryFeeRepository = deliveryFeeRepository;
			_discountRepository = discountRepository;
			_produtoRepository = produtoRepository;
			_orderRepository = orderRepository;
		}

		public ICommandResult Handle(CreateOrderCommand command) {

			command.Validate();
			if(command.Valid)
				return new GenericsCommandResult(false, "Pedido invalido", null);

			//Recupera o cliente
			var customer = _customerRepository.Get(command.Customer);

			//Calcula a taxa de entrega
			var deliveryFee = _deliveryFeeRepository.Get(command.ZipCode);

			// Obtem o cupom de desconto
			var discount = _discountRepository.Get(command.PromoCode);

			// Gera o pedido

			var products = _produtoRepository.Get(ExtractGuids.Extract(command.Items)).ToList();
			var order = new Order(customer, deliveryFee, discount);
			foreach(var item in command.Items) {
				var product = products.Where(x => x.Id == item.Product).FirstOrDefault();
				order.AddItem(product, item.Quantity);
			}

			//AddNotifications(customer.Notifications);
			AddNotifications(order.Notifications);

			if(Invalid)
				return new GenericsCommandResult(false, "Falha ao gerar o pedido", Notifications);

			_orderRepository.Save(order);
			return new GenericsCommandResult(true, $"Pedido {order.Number} gerado com sucesso", order);
		}
	}
}
