﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TripPlaner.Startup))]
namespace TripPlaner
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseAutofacContainer();
            ConfigureAuth(app);
        }
    }
}
