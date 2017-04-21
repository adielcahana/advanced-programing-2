using Server.Commands;
using System.Collections.Generic;

namespace Server
{
    /// <summary>
    /// server controller handle the commands before the multiple game or one player game commands
    /// </summary>
    /// <seealso cref="Server.Controller" />
    class ServerController : Controller
    {
        /// <summary>
        /// The model
        /// </summary>
        private IModel _model;
        /// <summary>
        /// constructor of the <see cref="ServerController"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public ServerController(IModel model)
        {
            _model = model;
            // addcommands to the base commands dicitionary
            commands.Add("generate", new Generate(model));
            commands.Add("solve", new Solve(model));
            commands.Add("start", new Start(model));
            commands.Add("join", new Join(model));
            commands.Add("list", new List(model));
        }
    }
}
