using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Commands;
using Store.Domain.Handlers;
using Store.Domain.Repositories.Interfaces;
using Store.Tests.Repositories;
using System;

namespace Store.Tests.Handler {
	[TestClass]
	public class OrderHandlerTest {

		private readonly ICustomerRepository _customerRepository;
		private readonly IDeliveryFeeRepository _deliveryFeeRepository;
		private readonly IDiscountRepository _discountRepository;
		private readonly IProductRepository _produtoRepository;
		private readonly IOrderRepository _orderRepository;

		public OrderHandlerTest() {
			_customerRepository = new FakeCustomerRepository();
			_deliveryFeeRepository = new FakeDeliveryFeeRepository();
			_discountRepository = new FakeDiscountRepository();
			_produtoRepository = new FakeProductRepository();
			_orderRepository = new FakeOrderRepository();
		}

		[TestMethod]
		[TestCategory("Handlers")]
		public void Dado_um_cliente_inexistente_o_pedido_nao_deve_ser_gerado() {

			Assert.Fail();
		}

		[TestMethod]
		[TestCategory("Handlers")]
		public void Dado_um_cep_invalido_o_pedido_deve_ser_gerado_normalmente() {

			Assert.Fail();
		}

		[TestMethod]
		[TestCategory("Handlers")]
		public void Dado_um_promocode_inexistente_o_pedido_deve_ser_gerado_normalmente() {

			Assert.Fail();
		}

		[TestMethod]
		[TestCategory("Handlers")]
		public void Dado_um_pedido_sem_itens_o_mesmo_nao_deve_ser_gerado() {

			Assert.Fail();
		}

		[TestMethod]
		[TestCategory("Handlers")]
		public void Dado_um_comando_invalido_o_pedido_nao_deve_ser_gerado() {

			Assert.Fail();
		}

		[TestMethod]
		[TestCategory("Handlers")]
		public void Dado_um_comando_valido_o_pedido_deve_ser_gerado() {

			var command = new CreateOrderCommand();

			command.Customer = "12345678";
			command.ZipCode = "13411080";
			command.PromoCode = "12345678";
			command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
			command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

			var handler = new OrderHandler(_customerRepository, _deliveryFeeRepository, _discountRepository, _produtoRepository, _orderRepository);
			handler.Handle(command);
			Assert.AreEqual(handler.Valid, true);

		}

	}
}
